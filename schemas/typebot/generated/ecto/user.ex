defmodule Typebot.User do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          last_activity_at: DateTime.t(),
          name: String.t() | nil,
          email: String.t() | nil,
          email_verified: DateTime.t() | nil,
          image: String.t() | nil,
          company: String.t() | nil,
          onboarding_categories: map(),
          referral: String.t() | nil,
          graph_navigation: atom() | nil,
          preferred_app_appearance: String.t() | nil,
          displayed_in_app_notifications: map() | nil,
          group_titles_auto_generation: map() | nil,
          preferred_language: String.t() | nil,
          terms_accepted_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          accounts: [Typebot.Account.t()],
          api_tokens: [Typebot.ApiToken.t()],
          collaborators_on_typebots: [Typebot.CollaboratorsOnTypebots.t()],
          workspaces: [Typebot.MemberInWorkspace.t()],
          sessions: [Typebot.Session.t()],
          banned_ips: [Typebot.BannedIp.t()],
          credentials: [Typebot.UserCredentials.t()]
        }

  schema "User" do
    field(:id, :string, primary_key: true)
    field(:last_activity_at, Typebot.PrismaDateTime, autogenerate: true, source: :lastActivityAt)
    field(:name, :string)
    field(:email, :string)
    field(:email_verified, Typebot.PrismaDateTime, source: :emailVerified)
    field(:image, :string)
    field(:company, :string)
    field(:onboarding_categories, :map, source: :onboardingCategories)
    field(:referral, :string)
    field(:graph_navigation, Ecto.Enum, values: [:MOUSE, :TRACKPAD], source: :graphNavigation)
    field(:preferred_app_appearance, :string, source: :preferredAppAppearance)
    field(:displayed_in_app_notifications, :map, source: :displayedInAppNotifications)
    field(:group_titles_auto_generation, :map, source: :groupTitlesAutoGeneration)
    field(:preferred_language, :string, source: :preferredLanguage)
    field(:terms_accepted_at, Typebot.PrismaDateTime, source: :termsAcceptedAt)
    has_many(:accounts, Typebot.Account, foreign_key: :user_id)
    has_many(:api_tokens, Typebot.ApiToken, foreign_key: :owner_id)
    has_many(:collaborators_on_typebots, Typebot.CollaboratorsOnTypebots, foreign_key: :user_id)
    has_many(:workspaces, Typebot.MemberInWorkspace, foreign_key: :user_id)
    has_many(:sessions, Typebot.Session, foreign_key: :user_id)
    has_many(:banned_ips, Typebot.BannedIp, foreign_key: :user_id)
    has_many(:credentials, Typebot.UserCredentials, foreign_key: :user_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end