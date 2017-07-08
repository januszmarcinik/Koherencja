USE [Mvc_Ankieta]
GO
/****** Object:  Schema [Dictionaries]    Script Date: 06.07.2017 21:44:39 ******/
CREATE SCHEMA [Dictionaries]
GO
/****** Object:  Schema [Identity]    Script Date: 06.07.2017 21:44:39 ******/
CREATE SCHEMA [Identity]
GO
/****** Object:  Schema [Questionnaire]    Script Date: 06.07.2017 21:44:39 ******/
CREATE SCHEMA [Questionnaire]
GO
/****** Object:  Table [Questionnaire].[Answers]    Script Date: 06.07.2017 21:44:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Questionnaire].[Answers](
	[AnswerId] [int] IDENTITY(1,1) NOT NULL,
	[OrderNumber] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Value] [int] NOT NULL,
	[QuestionId] [int] NOT NULL,
 CONSTRAINT [PK_Questionnaire.Answers] PRIMARY KEY CLUSTERED 
(
	[AnswerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Questionnaire].[Questionnaires]    Script Date: 06.07.2017 21:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Questionnaire].[Questionnaires](
	[QuestionnaireId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[OrderNumber] [int] NOT NULL,
	[EditDisable] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Questionnaire.Questionnaires] PRIMARY KEY CLUSTERED 
(
	[QuestionnaireId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Questionnaire].[Results]    Script Date: 06.07.2017 21:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Questionnaire].[Results](
	[ResultId] [int] IDENTITY(1,1) NOT NULL,
	[IntervieweeId] [int] NOT NULL,
	[QuestionnaireId] [int] NOT NULL,
	[QuestionId] [int] NOT NULL,
	[AnswerId] [int] NOT NULL,
 CONSTRAINT [PK_Questionnaire.Results] PRIMARY KEY CLUSTERED 
(
	[ResultId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Questionnaire].[Interviewees]    Script Date: 06.07.2017 21:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Questionnaire].[Interviewees](
	[IntervieweeId] [int] IDENTITY(1,1) NOT NULL,
	[InterviewDate] [datetime] NOT NULL,
	[Age] [int] NOT NULL,
	[SexId] [int] NOT NULL,
	[SeniorityId] [int] NOT NULL,
	[EducationId] [int] NOT NULL,
	[PlaceOfResidenceId] [int] NOT NULL,
	[MartialStatusId] [int] NOT NULL,
	[MaterialStatusId] [int] NOT NULL,
 CONSTRAINT [PK_Questionnaire.Interviewees] PRIMARY KEY CLUSTERED 
(
	[IntervieweeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Dictionaries].[BaseDictionaries]    Script Date: 06.07.2017 21:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Dictionaries].[BaseDictionaries](
	[BaseDictionaryId] [int] IDENTITY(1,1) NOT NULL,
	[DictionaryType] [int] NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_Dictionaries.BaseDictionaries] PRIMARY KEY CLUSTERED 
(
	[BaseDictionaryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  View [Questionnaire].[SummaryResultsView]    Script Date: 06.07.2017 21:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Questionnaire].[SummaryResultsView] AS
SELECT 
	Interviewee.IntervieweeId,
	Interviewee.InterviewDate,
	Interviewee.Age,
	Sex.Value AS 'Sex',
	Seniority.Value AS 'Seniority',
	PlaceOfResidence.Value AS 'PlaceOfResidence',
	Education.Value AS 'Education',
	MartialStatus.Value AS 'MartialStatus',
	MaterialStatus.Value AS 'MaterialStatus',
	Questionnaire.Name AS 'QuestionnaireName',
	SUM(Answers.Value) AS 'PointsSum'

FROM [Questionnaire].[Interviewees] Interviewee

INNER JOIN [Dictionaries].[BaseDictionaries] Sex ON Sex.BaseDictionaryId = Interviewee.SexId
INNER JOIN [Dictionaries].[BaseDictionaries] Seniority ON Seniority.BaseDictionaryId = Interviewee.SeniorityId
INNER JOIN [Dictionaries].[BaseDictionaries] PlaceOfResidence ON PlaceOfResidence.BaseDictionaryId = Interviewee.PlaceOfResidenceId
INNER JOIN [Dictionaries].[BaseDictionaries] Education ON Education.BaseDictionaryId = Interviewee.EducationId
INNER JOIN [Dictionaries].[BaseDictionaries] MartialStatus ON MartialStatus.BaseDictionaryId = Interviewee.MartialStatusId
INNER JOIN [Dictionaries].[BaseDictionaries] MaterialStatus ON MaterialStatus.BaseDictionaryId = Interviewee.MaterialStatusId

LEFT JOIN [Questionnaire].[Results] Results ON Results.IntervieweeId = Interviewee.IntervieweeId
LEFT JOIN [Questionnaire].[Answers] Answers ON Results.AnswerId = Answers.AnswerId

INNER JOIN [Questionnaire].[Questionnaires] Questionnaire ON Questionnaire.QuestionnaireId = Results.QuestionnaireId

GROUP BY

	Interviewee.IntervieweeId,
	Interviewee.InterviewDate,
	Interviewee.Age,
	Sex.Value,
	Seniority.Value,
	PlaceOfResidence.Value,
	Education.Value,
	MartialStatus.Value,
	MaterialStatus.Value,
	Questionnaire.Name


GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 06.07.2017 21:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Identity].[Roles]    Script Date: 06.07.2017 21:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Identity].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Identity.Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Identity].[UserClaims]    Script Date: 06.07.2017 21:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Identity].[UserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_Identity.UserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Identity].[UserLogins]    Script Date: 06.07.2017 21:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Identity].[UserLogins](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoginProvider] [nvarchar](max) NULL,
	[ProviderKey] [nvarchar](max) NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Identity.UserLogins] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Identity].[UserRoles]    Script Date: 06.07.2017 21:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Identity].[UserRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_Identity.UserRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Identity].[Users]    Script Date: 06.07.2017 21:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Identity].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Identity.Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Questionnaire].[Questions]    Script Date: 06.07.2017 21:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Questionnaire].[Questions](
	[QuestionId] [int] IDENTITY(1,1) NOT NULL,
	[OrderNumber] [int] NOT NULL,
	[Text] [nvarchar](max) NULL,
	[QuestionnaireId] [int] NOT NULL,
 CONSTRAINT [PK_Questionnaire.Questions] PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201707051953210_AutomaticMigration', N'JanuszMarcinik.Mvc.Domain.Application.Migrations.Configuration', 0x1F8B0800000000000400ED5D5B6FDCBA117E2FD0FF20EC535BE478E3A42D5AC33E073EBEB46EE324CD3A077D33E45D7A2D1CADB495B4C9BA457F591FFA93FA174ADD791B5E2491D2064680C02B92C399E1A721C51972FEF79FFF9EFEB0DF84DE1794A4411C9DCD8E8F5ECF3C142DE35510ADCF66BBECF1BB3FCC7EF8FE97BF38BD5A6DF6DE4F75BDB7793DDC324ACF664F59B63D99CFD3E513DAF8E9D1265826711A3F6647CB7833F757F1FCCDEBD77F9C1F1FCF112631C3B43CEFF4D32ECA820D2A7EE09F1771B444DB6CE787B7F10A8569F51C972C0AAADE7B7F83D2ADBF4467B3BFF8D12EFDE7AD9F2C8328F8F9E8F6CBF2E832DEF8417474E967FECC3B0F031F73B540E1E3CCF3A328CEFC0CF37CF239458B2C89A3F5628B1FF8E1DDF316E17A8F7E98A24A9693B6BAAE58AFDFE462CDDB8635A9E52ECD305B66048FDF567A9AB3CD3B697BD6E8116BF20A6B3C7BCEA52EB479363B8FD2AF2899796C57271761925793A9FA7CBB0D836551FDA8201CA0F4E86F3B94E64F223F48F0CF92FE2B0FA4F2AAC113865DFEEF9577B10BB35D82CE22B4CB123F7CE57DDC3DE08EFE8A9EEFE29F517416EDC290940ACB85CBA807F8D1C724DEA2247BFE841E29596F56336F4EB79EB3CD9BC65CCB52273751F6F6CDCC7B8F19F11F42D40088D0DF228B13F42714A1C4CFD0EAA39F6528C1E37FB342C510703C303D7E48562879BFDB3CE48323EF544EE812A5CB24D896F02909E13700BFD833EFD6DFBF43D13A7B3A9BE13F67DE75B047ABFA4945FC7314603B801B65C94ED9D74F7E9857EAC36E0D1FB5AE193AEFFD2FC1BA503D4071E67D426151217D0AB6A56D68D07A5F8E708A9590C49B4F714834ACCBEEEFFC648D32CC570C5458C4BB6469C0DA279462A8A742CE4A9AF74D95962FBAA4E9B4E68A29AE9926793A9DB776406A1D5ADDD9B20FF5CF695808127DA636421FB913B41277689F59370FD4D00FF78237AF6EDFF79B7D934003606A7A0A81A5FC1535EEEB5F4236992AA03562EB0D69941A8D08CC125B06AB7350D354E9D6B67D2A7E4ECB4835AF51574BA5F91E0E67AEF2FFAD5B99C16CE2D52AC82E83346F5113FA31C648F6236352E7CB2CF8D29B8AD5A59CD288C92D42070306D907C8D00D69C0CA3E24568CAAA06075107B5612B167C84AFAD3B060252F5D4C57DBD295CDC23DA0E44B80BE22F325CBA0EB9FA13E943A7EDA9AADC6FA7DD2B04B1BE08B4797256208857C11E522E604C51C87A23AA66CEA7DA8EAACBCC00FD5BEACE92E64B50DAB7C112B6357DBB052A36FCBBA129D4CC3C43266CBD4CE1A593D0BC6F61237AFBBCDFFBE0BF295A3A9695BF7DC8C5AA07D5FF3BA4051102758F4BE84AE56BB12837D097D0CFD25FAF088DFAD008FCAB2F74484719EEFA42F300276697F621800DDA98106ACD19ED078FDE8A7E83258E64FFDE4F9BEA94C1930B01267C4E09AA67697D2AD0EEB540324E39FA9A91282AD6E2E0939B07AA2902D14B2D055D5C230F54DA561DF211D79D8363281B8BA2A89F806436EFB745820B15F4CB245942E8B8D2DD551775D39902A9AA8A5523159D554B97822D1E379AFE076AFC3E71EE0507BC944531C70D5D410CD7FD0BD4C63D944F3D465E5C45370B5786A7BCD7B6C7688729CB14586B332E5D773B0E5444EC0C34FD7AC69524FEC9DA66B6479C2568801CDEFDD266C5D61BA4FD94A718029BEEB94AD2550BF395B21123CC99B4E88819E34E653A24202D1EC693025EA72AD33292A39DD033CEA6FD0C661C70D84DA80B7F3604E6B1A135EA7CD8183771981A8FC9C429EE47CC8EEABE2168BED530E8144512FDCE52486C45E4DEF057F3AF8CBB5D5775323D7B6190D23700C098C1750E880E20AEB23B4EEC82E7AB988A3C720D9A046BAAECEE38F7E9A7E8D93D59FFDF4C93AEB0BB4DCE51FEA78B5B6D95AEFEDE3531C21DAE3EFA2AFC186E6EE6B7CED2F3124AFA2BC556F7AEFE2E5CFF12EBB8A56F9D6F9E76CC9EFA46B1218849DF3255EDBA6D718CC687511EFA2AC9F35CDCD94E355C145E8071BF1B220E7E6BE2E6FD705C4636E61409699AE9ADFC5EB00F82A2EC8D6E50C27E56331275599719405A62061A42A66F8289E8AD9288B7AAF940ABD0E392316045FA645576BA542DDD4FE952D4B5EF4646367CB08AFC5DB37245E0B822F78D5C16BA12AFCEC4BB072B172A83AC21AB0DE97F99B088376B72120CB6E24DFA4D7A1BF6E4F50D9F114D0BD5A0037D62E1E99F0198F06A9325AC7B7285FF5911E9DC27A9CCD8EB9E160AA366EABAAC11B7903C2595D35782B6FC0BB23AB76BF95B763DCCB55A3DFA91AD19EDCAAD5EF793C95C8211F9EA769BC0C0AD998C8A226409FEE19AF613D55B47EEBE66823A86EF1A80739ACB0DAF301628DD587E812852843DE79812C3C1BF8E9D25FF12F161663A5CD52E3996B59AAC3E168867EC3F583AD264A72B3E5E79F7C29866A1065BC890DA265B0F543854E987642E30C1F75C9A56E7A624B2ED11645B97D55E8A03F0B4D4FCC80A834753A2750A6073E36785935E06024330FC52A62CE291EC19322EA37656060028A32010770CAA1134801C50CC48E03C0C29E4F08131A6ED01615ACDF9F452D8B0D9D6E04E0A32232ADE04F29B5CE90AB62018C20A854900E47540CE01400C87AAF35E101BAB22D40118C731B118F80FC63831250950E5B5C10E834D0C984236843068A4DB0824F28767154808A35303E42C5CAD283281B5A3C058CF211269AB891849B5840A92420754498823A181BA7A0BA74181305E84F01AA64F890266C84B14416E0290CE31D119802B9C786A440453A2C51E757A681C2BD09FEF6D691B79F0CE6F65342DBDE1467FBD110263ADE000DBFF4AC83D6E85BDAFC919E526D19AB8F995B41A444393A18909E593442A344198330E27C6F52054BC5B505DAFB92B0DD539CDF758430A99CE36E2B76C0D9343615B9A3E54A7F8706BE5CBB60C606641F2C0EE582E981C0D1C0C7DC09018D3374E79DCAD126B368D0A5138E9023164967D0A07B2C8D502396BE6FF70E104304E243232B8ACA2746B508A677629E0467000846DAC87E2B00E3B5A0B5F6E9072B5E629D4EEB08F951004586704203298CE7A447D211A444D1A30C27550CA415500914E1005502A1757AADC3A0C68355158F2B1D4C3638773C5831A1C00C2755A8A23D58D18A70052B5AE8C38055195D2D1D4B26D47A3C50D181DDEEA63F5E0BAE1045493C314095519EB84D865B34317D4408E6E5435E585C07CC0573623EAB78CEB48A4365473E27BE4019B5524C675E1B5BCAACD6B9C5394D80B84F9223D17E696A1229AFEA9250AAF64414E49A0B50383AF5E7828200B1BB24A2426D1D2A4851BBAE81901CBBD3AD92AE3CB7C1CB56BCA88AC6F51B2D22D0BEED1A4420025A8DEBD338420AD5DA48834C7D944648A69A0B1932C43BC7C3AFBD829AA8065D53CD1A028D38D84606EAB5E14C8A46F82A41A9E199B5FAB4A8066AE02E8A85B5210DCB946C4EF2819902DDD4D6404341503CA58EC63B684A72BD09AF2BCD88404A2E754C2021196F62241A5387011294693B38B4DAB88B5594BA9306B3C9C484C2D906D22214C1E65095ECB52E1ABA94855EC9A50582AF06D326106FE54C9D824B6594FA548409C90486038506D2281C1BE44CA5D4CD364A6582812C322945A12C03295014BDE250757B4DA57171177299F63614B577A222E17D87BC86947103942CB2C8014D5194F404AA69F81F780527D18B86EB5AB2CE92E8A6F3DACDA97E745463A0151D85682EF49DA881BDE59D5782CCE7A7E3F5537F9FE8F8F9AC084FDE12C50B0EB9AE54CE2B92D5F2F35A22AEC0E744B4273ED17B0B4BDD7DC14B0BFA55949E15865FB9BC22870843A0E6711889EB8D014062D196BF72D3DF5C6266AF9E2150F3388CC415600081059BD1AAED687371E95DE49E80AE0FBB37DBA04DD9E9BCCC355A3D389D0349494F6FFDED3688D64492D2EA89B72833945E7CB7304FD7B92969CC97943ED94DDBA6A72C4EFC35624A71D798D3EB2049B33C17EA839F9FCBBF586DB86AC24D5F605FACEE92DED7E5C7ACDE2AABEBE77F976DE4195BEB0D617EB7BCA2748D25DDE4FBECC59D0FDC0CC037F4F2B4B17EE82760BE938B38DC6D22554C034C894A344512A30AF4E951E99D487A54813EBDEA6E139252F5489F06191F441292C50DE5AF0D336A9C5F83430AE754A281A7054BD94E616760366E067368C24D8755B72B809649224942E513733835018F2221C168C8D181557D77584057E97AEA0E31A0BDAD7180E896F7B291C4CA27E361964AE247D2A30AF4E9D599FCA8C9A37A66DFD48F847FF84BAD33F02B5FA939E2A18690AADB8C71A49EA10C74324ACCA900929CE2C0004CD3D68B38EC4462BE661A09A8D4E6D98068257DF2E69095B676893626BD9690665964808D356B0DD746EDAB835F2405E02C988C06714891A62439BD08D3A3EED9A0270DC9051C303DD1895E92ACCE895F983A77F702495A7931838C2E7B609E26AC3A4E3F9A15E05C0C035A02269CC6DC18A80840A3C19FD62447437D9613A6CCDEE947AD4798323B5F9F632D6B808DABEE8B9A3CBEA9C39246D80C34E2EC6C6069853ED2A0B4FB79030E4C137C663E3870533B0354C7829214A0F850984A7DA0825AF302872C461DEAC187B9E310BB1ADE2A7500B5A8281F19D2206E9FE7881165068B142A4100B53CA14A4C96655416007A614615197049DEF54F31491674A20768545CC360D78CBBDD9FDA41E34AF5290BEEF927490B8A3BD016F0CC9699EC9E70A900E88D14AED8CC70F2B35BFB7452660F748BF6B27D657073370308B49DF22447DC154F12221E1BD212AC59C9E7934210E866EE85A032AEBD1B8280B67610C4DCDD4E9B27AAC8602E206F68A7A600B2C016CAAD6289F378B3559ADE6B0777FDBBF17857DE66CA0D5E489E3BB50B89D3CAF3CDBA9FCB2A33AF56239EFE9FD30C6D4AD82DFE115E8401CA8D7D5DE1D68F82479466E50DEDB337AF8FDFCCBCF330F0D332EEA072AC9FB00794B43CEDC76F734F3B5A6DE66C73737F7D4E254D575476043ECF83C845AD974101DA6255E751685B962757825CBFCA7C0986C93A289F0CD18F2119CAE9519289BE602BF5E427BFDAF8FB5F930475B21F50493DBA3044EE921354B8F37037D10AEDCF66FF2A9A9D78377FBF6F5BBEF20AED9C78AFBD7F0F95ED4DEC50D68312BCF3AF0613A08F89C2A9744A0F8023CEFFD20D0B55739B8010B87FCD5001B898F4A121D2D1D0F828D7F1030CEC4040A35CC8259987C09C4CED3EEE4EA19F01D5CF802B70BDEAC10C72B7AAF1D5B6B4082CC6A166F69A538D0D5EF209D99BD16740CDF58B46F7753B1BD616F4E36AA6C392B96D3532638120B5F63A94BEE0B2A715FE3B2B72479A8EE4BAD75AAC72069BA1A068D40B7F94E7D8B4F3A6692F162867B3190B44D35E2C881CD3669CF0147A31C4B9B3CDB8619AF7648575809BF242B7B761B164CE663DA3A5722CABED164FC1A2E962FDD5DDED0EF511696535C539155D64559CD2C2DC2851E637A42E3A3BA3AEBD285BF53259B507D8ACE3B2950DF3C43B5E0F764CA994F0BD3E4EC569DFBB7C168A52BEF7624D98D6BD174541EAF6A1E80DA24228357B175A605A76D1D25A4758719AF66E5B10408AF62E33279BA0DDD68C20F0581EACF9186D4AE0F26DF77AFDF89CDAB6065FE06C3CD8C117E6A1EE6706F95CD3BDE8398227848001D2068F79497D7DD921C18193B4C02E2FA107CFEF9AF638F5B4BF93C9F23B5A3EDFD1F26C1C504E8DBE897A074A5645DD254A50749D9CCA7D3E2AF9892DBEF36F24B5EE0B6E1CE3E65B4A7CFB021EE7E0F99652D2BEC0C7317CBE9D44B12FD0710C9D834EEBFA8216E76839B8E4AC236763AD5341104CB8CABCEA38D52A78914B874EA79D5675983CAAAE9131EED68D013AA6B171639C0C75CC8DE5B1B0E47263B903820E239969CFECA5AEC7DE60837F90FCA406E37E180949C7CA41DA265E22FA77907AD455BA35EDBEA0FB2F26956474CC547D556E2C8601DBD9445DE1043A1CCF77367E7E478DB4A16302A5CA7EC630603B3FA82BA04067E0A70814C14D4913C289EB69C7254AB4A79DD1937BF2D7C1B36346273FACD7A26D2C0EB3482DCFF3F35FC7655C0F943290EDA6FD56E23A6A8BE45DC159F7A0CEAAF6608F55B95EB7E2343440C25049BE50796F507212B61B6A938BEB8B2A9577284DA6C4F6CA6EBF721DB315EABEE91C5055D7AAE450C274A560B6D2BAAB3630AF56A9305F044BBB35561CFDB608EC03CE4B21EA07CC772AA5AF47BB5AC0093BA8CAA4BD00D953445D554B0061575599B42B206D89CBBCAAC2FC4A02EBCD7FCE13ED80F4C653CE9A0A27DA920A6FA4B6E927438553D5096EFE813C0F04095932E7034A717A606A7197AEF4A014E336F1E841A9C67E02D10353878DA4A0D35681EDA49FBAAC0B77F989A6F5A381D716B6F279765C57B8107CE0449DE62B479B420E9E86536381EB4C38C2E5D227CD269759532A10FB3D485F0FDE5FA8C1D269720C2AC5A23E11992B6107126C88AC99E682511FA4CC4DA50309567DE4F7488E692ED61030344882C95FFD793AFFB48BF233CBE5AF4BBC3C5CB7244E31CD082DA9EDC9A6CE4DF418D7BBA40C477515F6A61594F92B3FF3CFF1C7CBA3BFCC70717E5C3988D633AF386C9A1F9A7F40AB9BE8C32EDBEE322C32DA3C84D4A222DF6D95F55F64FAA4793EFD50DCA8960E21026633C88F797F887EDC05E1AAE1FB5A70081220916FE356874EF3B1CCF2C3A7EBE786D2FB38D22454A9AFD97DBE439B6D8889A51FA2859F5F4467CE1B86DF3BB4F697CFED295688887A2068B59F5E06FE3AF1376945A36D8F7F620CAF36FBEFFF0F40E7A4C3C3EA0000, N'6.1.3-40302')
SET IDENTITY_INSERT [Dictionaries].[BaseDictionaries] ON 

INSERT [Dictionaries].[BaseDictionaries] ([BaseDictionaryId], [DictionaryType], [Value]) VALUES (1, 1, N'Mężczyzna')
INSERT [Dictionaries].[BaseDictionaries] ([BaseDictionaryId], [DictionaryType], [Value]) VALUES (2, 1, N'Kobieta')
INSERT [Dictionaries].[BaseDictionaries] ([BaseDictionaryId], [DictionaryType], [Value]) VALUES (3, 2, N'0-5 lat')
INSERT [Dictionaries].[BaseDictionaries] ([BaseDictionaryId], [DictionaryType], [Value]) VALUES (4, 2, N'5-10 lat')
INSERT [Dictionaries].[BaseDictionaries] ([BaseDictionaryId], [DictionaryType], [Value]) VALUES (5, 2, N'10-15 lat')
INSERT [Dictionaries].[BaseDictionaries] ([BaseDictionaryId], [DictionaryType], [Value]) VALUES (6, 2, N'Powyżej 15 lat')
INSERT [Dictionaries].[BaseDictionaries] ([BaseDictionaryId], [DictionaryType], [Value]) VALUES (7, 3, N'Pielęgniarka dyplomowana')
INSERT [Dictionaries].[BaseDictionaries] ([BaseDictionaryId], [DictionaryType], [Value]) VALUES (8, 3, N'Licencjat pielęgniarstwa')
INSERT [Dictionaries].[BaseDictionaries] ([BaseDictionaryId], [DictionaryType], [Value]) VALUES (9, 3, N'Magister pielęgniarstwa')
INSERT [Dictionaries].[BaseDictionaries] ([BaseDictionaryId], [DictionaryType], [Value]) VALUES (10, 3, N'Specjalista pielęgniarstwa')
INSERT [Dictionaries].[BaseDictionaries] ([BaseDictionaryId], [DictionaryType], [Value]) VALUES (11, 4, N'Wieś')
INSERT [Dictionaries].[BaseDictionaries] ([BaseDictionaryId], [DictionaryType], [Value]) VALUES (12, 4, N'Miasto')
INSERT [Dictionaries].[BaseDictionaries] ([BaseDictionaryId], [DictionaryType], [Value]) VALUES (13, 5, N'W związku')
INSERT [Dictionaries].[BaseDictionaries] ([BaseDictionaryId], [DictionaryType], [Value]) VALUES (14, 5, N'Samotny/a')
INSERT [Dictionaries].[BaseDictionaries] ([BaseDictionaryId], [DictionaryType], [Value]) VALUES (15, 6, N'Bardzo dobry')
INSERT [Dictionaries].[BaseDictionaries] ([BaseDictionaryId], [DictionaryType], [Value]) VALUES (16, 6, N'Dobry')
INSERT [Dictionaries].[BaseDictionaries] ([BaseDictionaryId], [DictionaryType], [Value]) VALUES (17, 6, N'Średni')
INSERT [Dictionaries].[BaseDictionaries] ([BaseDictionaryId], [DictionaryType], [Value]) VALUES (18, 6, N'Zły')
INSERT [Dictionaries].[BaseDictionaries] ([BaseDictionaryId], [DictionaryType], [Value]) VALUES (19, 6, N'Bardzo zły')
SET IDENTITY_INSERT [Dictionaries].[BaseDictionaries] OFF
SET IDENTITY_INSERT [Identity].[Roles] ON 

INSERT [Identity].[Roles] ([Id], [Name]) VALUES (1, N'Ankieter')
INSERT [Identity].[Roles] ([Id], [Name]) VALUES (2, N'Administrator')
INSERT [Identity].[Roles] ([Id], [Name]) VALUES (3, N'Moderator')
SET IDENTITY_INSERT [Identity].[Roles] OFF
SET IDENTITY_INSERT [Identity].[UserRoles] ON 

INSERT [Identity].[UserRoles] ([Id], [UserId], [RoleId]) VALUES (1, 1, 1)
INSERT [Identity].[UserRoles] ([Id], [UserId], [RoleId]) VALUES (2, 2, 1)
INSERT [Identity].[UserRoles] ([Id], [UserId], [RoleId]) VALUES (3, 2, 2)
INSERT [Identity].[UserRoles] ([Id], [UserId], [RoleId]) VALUES (4, 2, 3)
INSERT [Identity].[UserRoles] ([Id], [UserId], [RoleId]) VALUES (5, 3, 1)
INSERT [Identity].[UserRoles] ([Id], [UserId], [RoleId]) VALUES (6, 3, 3)
SET IDENTITY_INSERT [Identity].[UserRoles] OFF
SET IDENTITY_INSERT [Identity].[Users] ON 

INSERT [Identity].[Users] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (1, N'ankieter@januszmarcinik.pl', 0, N'AD54hFOXCkN3h+eLtcSMMlAwxI4IkaE1WIIJkbi+5+hp8o4wB3P9Kn2ACWabmQe6/w==', N'cdd564cb-f5fc-46c6-a799-30bde1825cc5', NULL, 0, 0, NULL, 1, 0, N'ankieter@januszmarcinik.pl')
INSERT [Identity].[Users] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (2, N'admin@januszmarcinik.pl', 0, N'AI7xG8ljvzq7z4NpaTcXQ8rObzn1IRIHttmzPtMorNZUbWjI7wJcyBq9nhu/w+Lfbg==', N'fabafc02-6d40-4bc6-85fc-d80e0b093c19', NULL, 0, 0, NULL, 1, 0, N'admin@januszmarcinik.pl')
INSERT [Identity].[Users] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (3, N'moderator@januszmarcinik.pl', 0, N'AKRF6Fg/F8Hb1cSTSzspGwX1v+Vgv8blnzOo9KjX9H917P+5W0KWVz05Tj+b0Pl5gQ==', N'f3c35c44-083e-401f-9bdb-c5ce5129739a', NULL, 0, 0, NULL, 1, 0, N'moderator@januszmarcinik.pl')
SET IDENTITY_INSERT [Identity].[Users] OFF
SET IDENTITY_INSERT [Questionnaire].[Answers] ON 

INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (1, 1, N'', 0, 1)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (2, 2, N'', 1, 1)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (3, 3, N'', 2, 1)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (4, 4, N'', 3, 1)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (5, 5, N'', 4, 1)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (6, 1, N'', 0, 2)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (7, 2, N'', 1, 2)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (8, 3, N'', 2, 2)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (9, 4, N'', 3, 2)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (10, 5, N'', 4, 2)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (11, 1, N'', 0, 3)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (12, 2, N'', 1, 3)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (13, 3, N'', 2, 3)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (14, 4, N'', 3, 3)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (15, 5, N'', 4, 3)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (16, 1, N'', 0, 4)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (17, 2, N'', 1, 4)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (18, 3, N'', 2, 4)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (19, 4, N'', 3, 4)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (20, 5, N'', 4, 4)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (21, 1, N'', 0, 5)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (22, 2, N'', 1, 5)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (23, 3, N'', 2, 5)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (24, 4, N'', 3, 5)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (25, 5, N'', 4, 5)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (26, 1, N'', 0, 6)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (27, 2, N'', 1, 6)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (28, 3, N'', 2, 6)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (29, 4, N'', 3, 6)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (30, 5, N'', 4, 6)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (31, 1, N'', 0, 7)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (32, 2, N'', 1, 7)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (33, 3, N'', 2, 7)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (34, 4, N'', 3, 7)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (35, 5, N'', 4, 7)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (36, 1, N'', 0, 8)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (37, 2, N'', 1, 8)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (38, 3, N'', 2, 8)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (39, 4, N'', 3, 8)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (40, 5, N'', 4, 8)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (41, 1, N'', 0, 9)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (42, 2, N'', 1, 9)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (43, 3, N'', 2, 9)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (44, 4, N'', 3, 9)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (45, 5, N'', 4, 9)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (46, 1, N'', 0, 10)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (47, 2, N'', 1, 10)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (48, 3, N'', 2, 10)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (49, 4, N'', 3, 10)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (50, 5, N'', 4, 10)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (51, 1, N'', 1, 11)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (52, 2, N'', 2, 11)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (53, 3, N'', 3, 11)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (54, 4, N'', 4, 11)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (55, 5, N'', 5, 11)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (56, 1, N'', 1, 12)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (57, 2, N'', 2, 12)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (58, 3, N'', 3, 12)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (59, 4, N'', 4, 12)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (60, 5, N'', 5, 12)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (61, 1, N'', 1, 13)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (62, 2, NULL, 2, 13)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (63, 3, N'', 3, 13)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (64, 4, N'', 4, 13)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (65, 5, NULL, 5, 13)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (66, 1, N'', 1, 14)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (67, 2, N'', 2, 14)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (68, 3, N'', 3, 14)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (69, 4, N'', 4, 14)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (70, 5, N'', 5, 14)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (71, 1, N'', 1, 15)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (72, 2, N'', 2, 15)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (73, 3, N'', 3, 15)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (74, 4, N'', 4, 15)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (75, 5, N'', 5, 15)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (76, 1, N'', 1, 16)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (77, 2, N'', 2, 16)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (78, 3, N'', 3, 16)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (79, 4, N'', 4, 16)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (80, 5, N'', 5, 16)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (81, 1, N'', 1, 17)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (82, 2, N'', 2, 17)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (83, 3, N'', 3, 17)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (84, 4, N'', 4, 17)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (85, 5, N'', 5, 17)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (86, 1, N'', 1, 18)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (87, 2, N'', 2, 18)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (88, 3, N'', 3, 18)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (89, 4, N'', 4, 18)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (90, 5, N'', 5, 18)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (91, 1, N'', 1, 19)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (92, 2, N'', 2, 19)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (93, 3, N'', 3, 19)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (94, 4, N'', 4, 19)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (95, 5, N'', 5, 19)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (96, 1, N'', 1, 20)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (97, 2, N'', 2, 20)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (98, 3, N'', 3, 20)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (99, 4, N'', 4, 20)
GO
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (100, 5, N'', 5, 20)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (101, 1, N'', 1, 21)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (102, 2, N'', 2, 21)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (103, 3, N'', 3, 21)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (104, 4, N'', 4, 21)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (105, 5, N'', 5, 21)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (106, 1, N'', 1, 22)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (107, 2, N'', 2, 22)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (108, 3, N'', 3, 22)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (109, 4, N'', 4, 22)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (110, 5, N'', 5, 22)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (111, 1, N'', 1, 23)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (112, 2, N'', 2, 23)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (113, 3, N'', 3, 23)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (114, 4, N'', 4, 23)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (115, 5, N'', 5, 23)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (116, 1, N'', 1, 24)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (117, 2, N'', 2, 24)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (118, 3, N'', 3, 24)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (119, 4, N'', 4, 24)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (120, 5, N'', 5, 24)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (121, 1, N'', 1, 25)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (122, 2, N'', 2, 25)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (123, 3, N'', 3, 25)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (124, 4, N'', 4, 25)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (125, 5, N'', 5, 25)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (126, 1, N'', 1, 26)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (127, 2, N'', 2, 26)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (128, 3, N'', 3, 26)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (129, 4, N'', 4, 26)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (130, 5, N'', 5, 26)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (131, 1, N'', 1, 27)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (132, 2, N'', 2, 27)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (133, 3, N'', 3, 27)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (134, 4, N'', 4, 27)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (135, 5, N'', 5, 27)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (136, 1, N'', 1, 28)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (137, 2, N'', 2, 28)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (138, 3, N'', 3, 28)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (139, 4, N'', 4, 28)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (140, 5, N'', 5, 28)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (141, 1, N'', 1, 29)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (142, 2, N'', 2, 29)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (143, 3, N'', 3, 29)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (144, 4, N'', 4, 29)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (145, 5, N'', 5, 29)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (146, 1, N'', 1, 30)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (147, 2, N'', 2, 30)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (148, 3, N'', 3, 30)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (149, 4, N'', 4, 30)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (150, 5, N'', 5, 30)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (151, 1, N'', 1, 31)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (152, 2, N'', 2, 31)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (153, 3, N'', 3, 31)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (154, 4, N'', 4, 31)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (155, 5, N'', 5, 31)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (156, 1, N'', 1, 32)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (157, 2, N'', 2, 32)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (158, 3, N'', 3, 32)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (159, 4, N'', 4, 32)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (160, 5, N'', 5, 32)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (161, 1, N'', 1, 33)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (162, 2, N'', 2, 33)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (163, 3, N'', 3, 33)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (164, 4, N'', 4, 33)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (165, 5, N'', 5, 33)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (166, 1, N'', 1, 34)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (167, 2, N'', 2, 34)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (168, 3, N'', 3, 34)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (169, 4, N'', 4, 34)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (170, 5, N'', 5, 34)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (171, 1, N'Bardzo zła', 1, 35)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (172, 2, N'Zła', 2, 35)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (173, 3, N'Ani dobra, ani zła', 3, 35)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (174, 4, N'Dobra', 4, 35)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (175, 5, N'Bardzo dobra', 5, 35)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (176, 1, N'Bardzo nie-zadowolony/a', 1, 36)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (177, 2, N'Nie zadowolony/a', 2, 36)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (178, 3, N'Ani zadowolony/a, ani nie-zadowolony/a', 3, 36)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (179, 4, N'Zadowolony/a', 4, 36)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (180, 5, N'Bardzo zadowolony/a', 5, 36)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (181, 1, N'Wcale', 5, 37)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (182, 2, N'Nieco', 4, 37)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (183, 3, N'Średnio', 3, 37)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (184, 4, N'W dużym stopniu', 2, 37)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (185, 5, N'W bardzo dużym stopniu', 1, 37)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (186, 1, N'Wcale', 5, 38)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (187, 2, N'Nieco', 4, 38)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (188, 3, N'Średnio', 3, 38)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (189, 4, N'W dużym stopniu', 2, 38)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (190, 5, N'W bardzo dużym stopniu', 1, 38)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (191, 1, N'Wcale', 1, 39)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (192, 2, N'Nieco', 2, 39)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (193, 3, N'Średnio', 3, 39)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (194, 4, N'W dużym stopniu', 4, 39)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (195, 5, N'W bardzo dużym stopniu', 5, 39)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (196, 1, N'Wcale', 1, 40)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (197, 2, N'Nieco', 2, 40)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (198, 3, N'Średnio', 3, 40)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (199, 4, N'W dużym stopniu', 4, 40)
GO
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (200, 5, N'W bardzo dużym stopniu', 5, 40)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (201, 1, N'Wcale', 1, 41)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (202, 2, N'Nieco', 2, 41)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (203, 3, N'Średnio', 3, 41)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (204, 4, N'Dość dobrze', 4, 41)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (205, 5, N'Bardzo dobrze', 5, 41)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (216, 1, N'Wcale', 1, 42)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (217, 2, N'Nieco', 2, 42)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (218, 3, N'Średnio', 3, 42)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (219, 4, N'Dość dobrze', 4, 42)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (220, 5, N'Bardzo dobrze', 5, 42)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (221, 1, N'Wcale', 1, 43)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (222, 2, N'Nieco', 2, 43)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (223, 3, N'Średnio', 3, 43)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (224, 4, N'Dość dobrze', 4, 43)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (225, 5, N'Bardzo dobrze', 5, 43)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (226, 1, N'Wcale', 1, 44)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (227, 2, N'Nieco', 2, 44)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (228, 3, N'Umiarkowanie', 3, 44)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (229, 4, N'Przeważnie', 4, 44)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (230, 5, N'W pełni', 5, 44)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (231, 1, N'Wcale', 1, 45)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (232, 2, N'Nieco', 2, 45)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (233, 3, N'Umiarkowanie', 3, 45)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (234, 4, N'Przeważnie', 4, 45)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (235, 5, N'W pełni', 5, 45)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (236, 1, N'Wcale', 1, 46)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (237, 2, N'Nieco', 2, 46)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (238, 3, N'Umiarkowanie', 3, 46)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (239, 4, N'Przeważnie', 4, 46)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (240, 5, N'W pełni', 5, 46)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (241, 1, N'Wcale', 1, 47)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (242, 2, N'Nieco', 2, 47)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (243, 3, N'Umiarkowanie', 3, 47)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (244, 4, N'Przeważnie', 4, 47)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (245, 5, N'W pełni', 5, 47)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (246, 1, N'Wcale', 1, 48)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (247, 2, N'Nieco', 2, 48)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (248, 3, N'Umiarkowanie', 3, 48)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (249, 4, N'Przeważnie', 4, 48)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (250, 5, N'W pełni', 5, 48)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (251, 1, N'Bardzo źle', 1, 49)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (252, 2, N'Źle', 2, 49)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (253, 3, N'Ani dobrze ani źle', 3, 49)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (254, 4, N'Dobrze', 4, 49)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (255, 5, N'Bardzo dobrze', 5, 49)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (256, 1, N'Bardzo niezadowolony/a', 1, 50)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (257, 2, N'Niezadowolony/a', 2, 50)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (258, 3, N'Ani zadowolony/a ani niezadowolony/a', 3, 50)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (259, 4, N'Zadowolony/a', 4, 50)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (260, 5, N'Bardzo zadowolony/a', 5, 50)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (261, 1, N'Bardzo niezadowolony/a', 1, 51)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (262, 2, N'Niezadowolony/a', 2, 51)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (263, 3, N'Ani zadowolony/a ani niezadowolony/a', 3, 51)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (264, 4, N'Zadowolony/a', 4, 51)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (265, 5, N'Bardzo zadowolony/a', 5, 51)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (266, 1, N'Bardzo niezadowolony/a', 1, 52)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (267, 2, N'Niezadowolony/a', 2, 52)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (268, 3, N'Ani zadowolony/a ani niezadowolony/a', 3, 52)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (269, 4, N'Zadowolony/a', 4, 52)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (270, 5, N'Bardzo zadowolony/a', 5, 52)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (271, 1, N'Bardzo niezadowolony/a', 1, 53)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (272, 2, N'Niezadowolony/a', 2, 53)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (273, 3, N'Ani zadowolony/a ani niezadowolony/a', 3, 53)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (274, 4, N'Zadowolony/a', 4, 53)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (275, 5, N'Bardzo zadowolony/a', 5, 53)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (276, 1, N'Bardzo niezadowolony/a', 1, 54)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (277, 2, N'Niezadowolony/a', 2, 54)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (278, 3, N'Ani zadowolony/a ani niezadowolony/a', 3, 54)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (279, 4, N'Zadowolony/a', 4, 54)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (280, 5, N'Bardzo zadowolony/a', 5, 54)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (281, 1, N'Bardzo niezadowolony/a', 1, 55)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (282, 2, N'Niezadowolony/a', 2, 55)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (283, 3, N'Ani zadowolony/a ani niezadowolony/a', 3, 55)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (284, 4, N'Zadowolony/a', 4, 55)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (285, 5, N'Bardzo zadowolony/a', 5, 55)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (286, 1, N'Bardzo niezadowolony/a', 1, 56)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (287, 2, N'Niezadowolony/a', 2, 56)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (288, 3, N'Ani zadowolony/a ani niezadowolony/a', 3, 56)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (289, 4, N'Zadowolony/a', 4, 56)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (290, 5, N'Bardzo zadowolony/a', 5, 56)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (291, 1, N'Bardzo niezadowolony/a', 1, 57)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (292, 2, N'Niezadowolony/a', 2, 57)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (293, 3, N'Ani zadowolony/a ani niezadowolony/a', 3, 57)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (294, 4, N'Zadowolony/a', 4, 57)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (295, 5, N'Bardzo zadowolony/a', 5, 57)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (296, 1, N'Bardzo niezadowolony/a', 1, 58)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (297, 2, N'Niezadowolony/a', 2, 58)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (298, 3, N'Ani zadowolony/a ani niezadowolony/a', 3, 58)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (299, 4, N'Zadowolony/a', 4, 58)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (300, 5, N'Bardzo zadowolony/a', 5, 58)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (301, 1, N'Bardzo niezadowolony/a', 1, 59)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (302, 2, N'Niezadowolony/a', 2, 59)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (303, 3, N'Ani zadowolony/a ani niezadowolony/a', 3, 59)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (304, 4, N'Zadowolony/a', 4, 59)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (305, 5, N'Bardzo zadowolony/a', 5, 59)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (306, 1, N'Nigdy', 5, 60)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (307, 2, N'Rzadko', 4, 60)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (308, 3, N'Często', 3, 60)
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (309, 4, N'Bardzo często', 2, 60)
GO
INSERT [Questionnaire].[Answers] ([AnswerId], [OrderNumber], [Description], [Value], [QuestionId]) VALUES (310, 5, N'Zawsze', 1, 60)
SET IDENTITY_INSERT [Questionnaire].[Answers] OFF
SET IDENTITY_INSERT [Questionnaire].[Interviewees] ON 

INSERT [Questionnaire].[Interviewees] ([IntervieweeId], [InterviewDate], [Age], [SexId], [SeniorityId], [EducationId], [PlaceOfResidenceId], [MartialStatusId], [MaterialStatusId]) VALUES (1, CAST(N'2017-07-05T23:42:53.090' AS DateTime), 22, 1, 5, 8, 12, 14, 18)
INSERT [Questionnaire].[Interviewees] ([IntervieweeId], [InterviewDate], [Age], [SexId], [SeniorityId], [EducationId], [PlaceOfResidenceId], [MartialStatusId], [MaterialStatusId]) VALUES (2, CAST(N'2017-07-06T21:23:03.577' AS DateTime), 22, 1, 4, 7, 11, 13, 15)
SET IDENTITY_INSERT [Questionnaire].[Interviewees] OFF
SET IDENTITY_INSERT [Questionnaire].[Questionnaires] ON 

INSERT [Questionnaire].[Questionnaires] ([QuestionnaireId], [Name], [OrderNumber], [EditDisable], [Active], [Description]) VALUES (1, N'Kwestionariusz poczucia koherencji', 1, 0, 1, N'Proszę ocenić w jakim stopniu podane poniżej stwierdzenia odnoszą się do Ciebie. Proszę być szczerym w swoich odpowiedziach i uważać, aby odpowiedź na jedno pytanie, nie wpływała na pozostałe. Nie ma odpowiedzi dobrych ani złych. <br/><br/>
<div class="text-center">
<strong>0</strong> - zdecydowanie <strong> nie odnosi się </strong> do mnie <br/>
<strong>1</strong> - raczej nie odnosi się do mnie <br/>
<strong>2</strong> – ani się odnosi ani się nie odnosi <br/>
<strong>3</strong> – raczej odnosi się do mnie <br/>
<strong>4</strong> - zdecydowanie <strong> odnosi się </strong> do mnie
</div>')
INSERT [Questionnaire].[Questionnaires] ([QuestionnaireId], [Name], [OrderNumber], [EditDisable], [Active], [Description]) VALUES (2, N'IZZ', 2, 0, 1, N'Poniżej podano przykłady różnych zachowań związanych ze zdrowiem. <strong>Jak często w ciągu roku przestrzega Pan/Pani wymienionych poniżej zachowań?</strong> Proszę odpowiedzieć szczerze wybierając liczbę wyrażającą właściwą dla siebie odpowiedź. <br/><br/>
<div class="text-center">
<strong>1</strong> - <strong>prawie nigdy</strong><br/>
<strong>2</strong> - rzadko <br/>
<strong>3</strong> - od czasu do czasu <br/>
<strong>4</strong> - często <br/>
<strong>5</strong> - <strong>prawie zawsze</strong>
</div>')
INSERT [Questionnaire].[Questionnaires] ([QuestionnaireId], [Name], [OrderNumber], [EditDisable], [Active], [Description]) VALUES (3, N'Ocena jakości życia', 3, 0, 1, N'<p>Kolejne pytania dotyczą jakości Pana/i życia, zdrowia i innych dziedzin.</p>
<p>Proszę wybrać najbardziej właściwą odpowiedź. Jeśli nie jest Pan/i pewien/a, która z odpowiedzi jest właściwa, to proszę podać pierwszą o której Pan/i pomyślał/a, z zasady jest ona najbliższa prawdy. Proszę myśleć o swoim poziomie życia, nadziejach, przyjemnościach i troskach. </p>
<p>Pytania dotyczą spraw życia z ostatnich czterech tygodni.</p>')
SET IDENTITY_INSERT [Questionnaire].[Questionnaires] OFF
SET IDENTITY_INSERT [Questionnaire].[Questions] ON 

INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (1, 1, N'W trudnych chwilach zazwyczaj oczekuję pomyślnego rozwiązania', 1)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (2, 2, N'Łatwo się relaksuję', 1)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (3, 3, N'Jeśli ma mnie spotkać niepowodzenie to mnie spotka', 1)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (4, 4, N'Zawsze patrzę w przyszłość optymistycznie', 1)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (5, 5, N'Towarzystwo moich przyjaciół sprawia mi dużą radość', 1)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (6, 6, N'Jest dla mnie ważne, aby mieć jakieś zajęcie', 1)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (7, 7, N'Prawie nigdy nie oczekuję, że sprawy ułożą się po mojej myśli', 1)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (8, 8, N'Trudno jest wytrącić mnie z równowagi', 1)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (9, 9, N'Rzadko liczę na to, że przytrafi mi się coś dobrego', 1)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (10, 10, N'Ogólnie oczekuję, że przytrafi mi się więcej dobrego niż złego', 1)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (11, 1, N'Jem dużo warzyw, owoców', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (12, 2, N'Unikam przeziębień', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (13, 3, N'Poważnie traktuję wskazówki osób wyrażających zaniepokojenie moim zdrowiem', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (14, 4, N'Wystarczająco dużo odpoczywam', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (15, 5, N'Ograniczam spożywanie takich produktów, jak tłuszcze zwierzęce, cukier', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (16, 6, N'Mam zanotowane numery telefonów służb pogotowia', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (17, 7, N'Unikam sytuacji, które wpływają na mnie przygnębiająco', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (18, 8, N'Unikam przepracowania', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (19, 9, N'Dbam o prawidłowe odżywianie', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (20, 10, N'Przestrzegam zaleceń lekarskich wynikających z moich badań', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (21, 11, N'Staram się unikać zbyt silnych emocji, stresów i napięć', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (22, 12, N'Kontroluję swoją wagę ciała', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (23, 13, N'Unikam spożywania żywności z konserwantami', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (24, 14, N'Regularnie zgłaszam się na badania lekarskie', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (25, 15, N'Mam przyjaciół i uregulowane życie rodzinne', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (26, 16, N'Wystarczająco dużo śpię', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (27, 17, N'Unikam soli i silnie solonej żywności', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (28, 18, N'Staram się dowiedzieć, jak inni unikają chorób', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (29, 19, N'Unikam takich uczuć, jak gniew, lęk i depresja', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (30, 20, N'Ograniczam palenie tytoniu', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (31, 21, N'Jem pieczywo pełnoziarniste', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (32, 22, N'Staram się uzyskać informacje medyczne i zrozumieć przyczyny zdrowia i choroby', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (33, 23, N'Myślę pozytywnie', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (34, 24, N'Unikam nadmiernego wysiłku fizycznego', 2)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (35, 1, N'Jaka jest Pana/i jakość życia?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (36, 2, N'Czy jest Pan/i zadowolony/a ze swojego zdrowia?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (37, 3, N'Jak bardzo ból fizyczny przeszkadzał Panu/i robić to, co Pan/i powinien/na?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (38, 4, N'W jakim stopniu potrzebuje Pan/i leczenia medycznego do codziennego funkcjonowania?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (39, 5, N'Ile ma Pan/i radości w życiu?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (40, 6, N'W jakim stopniu ocenia Pan/i, że Pana/i życie ma sens?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (41, 7, N'Czy dobrze koncentruje Pan/i uwagę?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (42, 8, N'Jak bezpiecznie czuje się Pan/i w swoim codziennym życiu?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (43, 9, N'W jakim stopniu Pańskie/Pani otoczenie sprzyja zdrowiu?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (44, 10, N'Czy ma Pan/i wystarczająco energii w codziennym życiu?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (45, 11, N'Czy jest Pan/i w stanie zaakceptować swój wygląd (fizyczny)?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (46, 12, N'Czy ma Pan/i wystarczająco dużo pieniędzy na swoje potrzeby?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (47, 13, N'Na ile dostępne są informacje, których może Pan/i potrzebować w codziennym życiu?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (48, 14, N'W jakim zakresie ma Pan/i sposobność realizowania swoich zainteresowań?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (49, 15, N'Jak odnajduje się Pan/i w tej sytuacji?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (50, 16, N'Czy zadowolony jest Pan/i ze swojego snu?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (51, 17, N'W jakim stopniu jest Pan/i zadowolony/a ze swojej wydolności w życiu codziennym?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (52, 18, N'W jakim stopniu jest Pan/i zadowolony ze swojej zdolności (gotowości) do pracy?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (53, 19, N'Czy jest Pan/i zadowolony/a z siebie?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (54, 20, N'Czy jest Pan/i zadowolony/a ze swoich osobistych relacji z ludźmi?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (55, 21, N'Czy jest Pan/i zadowolony/a ze swojego życia intymnego?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (56, 22, N'Czy jest Pan/i zadowolony/a z oparcia, wsparcia, jakie dostaje Pan/i od swoich przyjaciół?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (57, 23, N'Jak bardzo jest Pan/i zadowolony/a ze swoich warunków mieszkaniowych?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (58, 24, N'Jak bardzo jest Pan/i zadowolony/a z placówek służby zdrowia?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (59, 25, N'Czy jest Pan/i zadowolony z komunikacji (transportu)?', 3)
INSERT [Questionnaire].[Questions] ([QuestionId], [OrderNumber], [Text], [QuestionnaireId]) VALUES (60, 26, N'Jak często doświadczał Pana/Panią negatywnych uczuć, takich jak przygnębienie, rozpacz, lęk, depresja?', 3)
SET IDENTITY_INSERT [Questionnaire].[Questions] OFF
ALTER TABLE [Identity].[UserClaims]  WITH CHECK ADD  CONSTRAINT [FK_Identity.UserClaims_Identity.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [Identity].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Identity].[UserClaims] CHECK CONSTRAINT [FK_Identity.UserClaims_Identity.Users_UserId]
GO
ALTER TABLE [Identity].[UserLogins]  WITH CHECK ADD  CONSTRAINT [FK_Identity.UserLogins_Identity.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [Identity].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Identity].[UserLogins] CHECK CONSTRAINT [FK_Identity.UserLogins_Identity.Users_UserId]
GO
ALTER TABLE [Identity].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_Identity.UserRoles_Identity.Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [Identity].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Identity].[UserRoles] CHECK CONSTRAINT [FK_Identity.UserRoles_Identity.Roles_RoleId]
GO
ALTER TABLE [Identity].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_Identity.UserRoles_Identity.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [Identity].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Identity].[UserRoles] CHECK CONSTRAINT [FK_Identity.UserRoles_Identity.Users_UserId]
GO
ALTER TABLE [Questionnaire].[Answers]  WITH CHECK ADD  CONSTRAINT [FK_Questionnaire.Answers_Questionnaire.Questions_QuestionId] FOREIGN KEY([QuestionId])
REFERENCES [Questionnaire].[Questions] ([QuestionId])
ON DELETE CASCADE
GO
ALTER TABLE [Questionnaire].[Answers] CHECK CONSTRAINT [FK_Questionnaire.Answers_Questionnaire.Questions_QuestionId]
GO
ALTER TABLE [Questionnaire].[Interviewees]  WITH CHECK ADD  CONSTRAINT [FK_Questionnaire.Interviewees_Dictionaries.BaseDictionaries_EducationId] FOREIGN KEY([EducationId])
REFERENCES [Dictionaries].[BaseDictionaries] ([BaseDictionaryId])
GO
ALTER TABLE [Questionnaire].[Interviewees] CHECK CONSTRAINT [FK_Questionnaire.Interviewees_Dictionaries.BaseDictionaries_EducationId]
GO
ALTER TABLE [Questionnaire].[Interviewees]  WITH CHECK ADD  CONSTRAINT [FK_Questionnaire.Interviewees_Dictionaries.BaseDictionaries_MartialStatusId] FOREIGN KEY([MartialStatusId])
REFERENCES [Dictionaries].[BaseDictionaries] ([BaseDictionaryId])
GO
ALTER TABLE [Questionnaire].[Interviewees] CHECK CONSTRAINT [FK_Questionnaire.Interviewees_Dictionaries.BaseDictionaries_MartialStatusId]
GO
ALTER TABLE [Questionnaire].[Interviewees]  WITH CHECK ADD  CONSTRAINT [FK_Questionnaire.Interviewees_Dictionaries.BaseDictionaries_MaterialStatusId] FOREIGN KEY([MaterialStatusId])
REFERENCES [Dictionaries].[BaseDictionaries] ([BaseDictionaryId])
GO
ALTER TABLE [Questionnaire].[Interviewees] CHECK CONSTRAINT [FK_Questionnaire.Interviewees_Dictionaries.BaseDictionaries_MaterialStatusId]
GO
ALTER TABLE [Questionnaire].[Interviewees]  WITH CHECK ADD  CONSTRAINT [FK_Questionnaire.Interviewees_Dictionaries.BaseDictionaries_PlaceOfResidenceId] FOREIGN KEY([PlaceOfResidenceId])
REFERENCES [Dictionaries].[BaseDictionaries] ([BaseDictionaryId])
GO
ALTER TABLE [Questionnaire].[Interviewees] CHECK CONSTRAINT [FK_Questionnaire.Interviewees_Dictionaries.BaseDictionaries_PlaceOfResidenceId]
GO
ALTER TABLE [Questionnaire].[Interviewees]  WITH CHECK ADD  CONSTRAINT [FK_Questionnaire.Interviewees_Dictionaries.BaseDictionaries_SeniorityId] FOREIGN KEY([SeniorityId])
REFERENCES [Dictionaries].[BaseDictionaries] ([BaseDictionaryId])
GO
ALTER TABLE [Questionnaire].[Interviewees] CHECK CONSTRAINT [FK_Questionnaire.Interviewees_Dictionaries.BaseDictionaries_SeniorityId]
GO
ALTER TABLE [Questionnaire].[Interviewees]  WITH CHECK ADD  CONSTRAINT [FK_Questionnaire.Interviewees_Dictionaries.BaseDictionaries_SexId] FOREIGN KEY([SexId])
REFERENCES [Dictionaries].[BaseDictionaries] ([BaseDictionaryId])
GO
ALTER TABLE [Questionnaire].[Interviewees] CHECK CONSTRAINT [FK_Questionnaire.Interviewees_Dictionaries.BaseDictionaries_SexId]
GO
ALTER TABLE [Questionnaire].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questionnaire.Questions_Questionnaire.Questionnaires_QuestionnaireId] FOREIGN KEY([QuestionnaireId])
REFERENCES [Questionnaire].[Questionnaires] ([QuestionnaireId])
ON DELETE CASCADE
GO
ALTER TABLE [Questionnaire].[Questions] CHECK CONSTRAINT [FK_Questionnaire.Questions_Questionnaire.Questionnaires_QuestionnaireId]
GO
ALTER TABLE [Questionnaire].[Results]  WITH CHECK ADD  CONSTRAINT [FK_Questionnaire.Results_Questionnaire.Answers_AnswerId] FOREIGN KEY([AnswerId])
REFERENCES [Questionnaire].[Answers] ([AnswerId])
GO
ALTER TABLE [Questionnaire].[Results] CHECK CONSTRAINT [FK_Questionnaire.Results_Questionnaire.Answers_AnswerId]
GO
ALTER TABLE [Questionnaire].[Results]  WITH CHECK ADD  CONSTRAINT [FK_Questionnaire.Results_Questionnaire.Interviewees_IntervieweeId] FOREIGN KEY([IntervieweeId])
REFERENCES [Questionnaire].[Interviewees] ([IntervieweeId])
ON DELETE CASCADE
GO
ALTER TABLE [Questionnaire].[Results] CHECK CONSTRAINT [FK_Questionnaire.Results_Questionnaire.Interviewees_IntervieweeId]
GO
ALTER TABLE [Questionnaire].[Results]  WITH CHECK ADD  CONSTRAINT [FK_Questionnaire.Results_Questionnaire.Questionnaires_QuestionnaireId] FOREIGN KEY([QuestionnaireId])
REFERENCES [Questionnaire].[Questionnaires] ([QuestionnaireId])
GO
ALTER TABLE [Questionnaire].[Results] CHECK CONSTRAINT [FK_Questionnaire.Results_Questionnaire.Questionnaires_QuestionnaireId]
GO
ALTER TABLE [Questionnaire].[Results]  WITH CHECK ADD  CONSTRAINT [FK_Questionnaire.Results_Questionnaire.Questions_QuestionId] FOREIGN KEY([QuestionId])
REFERENCES [Questionnaire].[Questions] ([QuestionId])
ON DELETE CASCADE
GO
ALTER TABLE [Questionnaire].[Results] CHECK CONSTRAINT [FK_Questionnaire.Results_Questionnaire.Questions_QuestionId]
GO
