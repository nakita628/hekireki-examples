defmodule Postiz.User do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Postiz.PrismaDateTime, autogenerate: {Postiz.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          email: String.t(),
          password: String.t() | nil,
          provider_name: atom(),
          name: String.t() | nil,
          last_name: String.t() | nil,
          is_super_admin: boolean(),
          bio: String.t() | nil,
          audience: integer(),
          provider_id: String.t() | nil,
          timezone: integer(),
          timezone_name: String.t() | nil,
          last_read_notifications: DateTime.t(),
          invite_id: String.t() | nil,
          activated: boolean(),
          account: String.t() | nil,
          connected_account: boolean(),
          last_online: DateTime.t(),
          ip: String.t() | nil,
          agent: String.t() | nil,
          deleted_at: DateTime.t() | nil,
          send_success_emails: boolean(),
          send_failure_emails: boolean(),
          send_streak_emails: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          picture: Postiz.Media.t() | nil,
          agencies: Postiz.SocialMediaAgency.t() | nil,
          comments: [Postiz.Comments.t()],
          items: [Postiz.ItemUser.t()],
          group_buyer: [Postiz.MessagesGroup.t()],
          group_seller: [Postiz.MessagesGroup.t()],
          order_buyer: [Postiz.Orders.t()],
          order_seller: [Postiz.Orders.t()],
          payout_problems: [Postiz.PayoutProblems.t()],
          organizations: [Postiz.UserOrganization.t()],
          oauth_authorizations: [Postiz.OAuthAuthorization.t()]
        }

  schema "User" do
    field(:email, :string)
    field(:password, :string)
    field(:provider_name, Ecto.Enum, values: [:LOCAL, :GITHUB, :GOOGLE, :APPLE, :FARCASTER, :WALLET, :GENERIC], source: :providerName)
    field(:name, :string)
    field(:last_name, :string, source: :lastName)
    field(:is_super_admin, :boolean, default: false, source: :isSuperAdmin)
    field(:bio, :string)
    field(:audience, :integer, default: 0)
    field(:provider_id, :string, source: :providerId)
    field(:timezone, :integer)
    field(:timezone_name, :string, source: :timezoneName)
    field(:last_read_notifications, Postiz.PrismaDateTime, autogenerate: true, source: :lastReadNotifications)
    field(:invite_id, :string, source: :inviteId)
    field(:activated, :boolean, default: true)
    field(:account, :string)
    field(:connected_account, :boolean, default: false, source: :connectedAccount)
    field(:last_online, Postiz.PrismaDateTime, autogenerate: true, source: :lastOnline)
    field(:ip, :string)
    field(:agent, :string)
    field(:deleted_at, Postiz.PrismaDateTime, source: :deletedAt)
    field(:send_success_emails, :boolean, default: true, source: :sendSuccessEmails)
    field(:send_failure_emails, :boolean, default: true, source: :sendFailureEmails)
    field(:send_streak_emails, :boolean, default: true, source: :sendStreakEmails)
    field(:picture_id, :binary_id, source: :pictureId)
    belongs_to(:picture, Postiz.Media, foreign_key: :picture_id, define_field: false)
    has_one(:agencies, Postiz.SocialMediaAgency, foreign_key: :user_id)
    has_many(:comments, Postiz.Comments, foreign_key: :user_id)
    has_many(:items, Postiz.ItemUser, foreign_key: :user_id)
    has_many(:group_buyer, Postiz.MessagesGroup, foreign_key: :buyer_id)
    has_many(:group_seller, Postiz.MessagesGroup, foreign_key: :seller_id)
    has_many(:order_buyer, Postiz.Orders, foreign_key: :buyer_id)
    has_many(:order_seller, Postiz.Orders, foreign_key: :seller_id)
    has_many(:payout_problems, Postiz.PayoutProblems, foreign_key: :user_id)
    has_many(:organizations, Postiz.UserOrganization, foreign_key: :user_id)
    has_many(:oauth_authorizations, Postiz.OAuthAuthorization, foreign_key: :user_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end