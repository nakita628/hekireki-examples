defmodule TypeHero.User do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: TypeHero.PrismaDateTime, autogenerate: {TypeHero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          name: String.t(),
          email: String.t(),
          email_verified: DateTime.t() | nil,
          image: String.t() | nil,
          bio: String.t(),
          status: atom(),
          ban_reason: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          accounts: [TypeHero.Account.t()],
          sessions: [TypeHero.Session.t()],
          challenge: [TypeHero.Challenge.t()],
          vote: [TypeHero.Vote.t()],
          submission: [TypeHero.Submission.t()],
          bookmark: [TypeHero.Bookmark.t()],
          shared_solution: [TypeHero.SharedSolution.t()],
          comment: [TypeHero.Comment.t()],
          reports: [TypeHero.Report.t()],
          reports_about: [TypeHero.Report.t()],
          moderated: [TypeHero.Report.t()],
          image_upload: [TypeHero.ImageUpload.t()],
          beta_tokens: [TypeHero.BetaTokens.t()],
          donations: [TypeHero.Donation.t()],
          short_urls: [TypeHero.ShortURL.t()],
          to_user_notification: [TypeHero.Notification.t()],
          from_user_notification: [TypeHero.Notification.t()],
          roles: [TypeHero.Role.t()],
          user_links: [TypeHero.UserLink.t()],
          tracks: [TypeHero.Track.t()]
        }

  schema "User" do
    field(:name, :string)
    field(:email, :string)
    field(:email_verified, TypeHero.PrismaDateTime, source: :emailVerified)
    field(:image, :string)
    field(:bio, :string, default: "")
    field(:status, Ecto.Enum, values: [:BANNED, :ACTIVE], default: :ACTIVE)
    field(:ban_reason, :string, source: :banReason)
    has_many(:accounts, TypeHero.Account, foreign_key: :user_id)
    has_many(:sessions, TypeHero.Session, foreign_key: :user_id)
    has_many(:challenge, TypeHero.Challenge, foreign_key: :user_id)
    has_many(:vote, TypeHero.Vote, foreign_key: :user_id)
    has_many(:submission, TypeHero.Submission, foreign_key: :user_id)
    has_many(:bookmark, TypeHero.Bookmark, foreign_key: :user_id)
    has_many(:shared_solution, TypeHero.SharedSolution, foreign_key: :user_id)
    has_many(:comment, TypeHero.Comment, foreign_key: :user_id)
    has_many(:reports, TypeHero.Report, foreign_key: :reporter_id)
    has_many(:reports_about, TypeHero.Report, foreign_key: :user_id)
    has_many(:moderated, TypeHero.Report, foreign_key: :moderator_id)
    has_many(:image_upload, TypeHero.ImageUpload, foreign_key: :user_id)
    has_many(:beta_tokens, TypeHero.BetaTokens, foreign_key: :user_id)
    has_many(:donations, TypeHero.Donation, foreign_key: :user_id)
    has_many(:short_urls, TypeHero.ShortURL, foreign_key: :user_id)
    has_many(:to_user_notification, TypeHero.Notification, foreign_key: :to_user_id)
    has_many(:from_user_notification, TypeHero.Notification, foreign_key: :from_user_id)
    many_to_many(:roles, TypeHero.Role, join_through: "_RoleToUser", join_keys: [B: :id, A: :id])
    many_to_many(:user_links, TypeHero.UserLink, join_through: "_UserToUserLink", join_keys: [A: :id, B: :id])
    many_to_many(:tracks, TypeHero.Track, join_through: "_TrackToUser", join_keys: [B: :id, A: :id])
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end