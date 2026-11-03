defmodule Postiz.Organization do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Postiz.PrismaDateTime, autogenerate: {Postiz.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          name: String.t(),
          description: String.t() | nil,
          api_key: String.t() | nil,
          payment_id: String.t() | nil,
          streak_since: DateTime.t() | nil,
          deleted_at: DateTime.t() | nil,
          allow_trial: boolean(),
          is_trailing: boolean(),
          shortlink: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          subscription: Postiz.Subscription.t() | nil,
          auto_post: [Postiz.AutoPost.t()],
          comments: [Postiz.Comments.t()],
          credits: [Postiz.Credits.t()],
          clippings: [Postiz.Clipping.t()],
          customers: [Postiz.Customer.t()],
          errors: [Postiz.Errors.t()],
          github: [Postiz.GitHub.t()],
          integration: [Postiz.Integration.t()],
          media: [Postiz.Media.t()],
          buyer_organization: [Postiz.MessagesGroup.t()],
          notifications: [Postiz.Notifications.t()],
          plugs: [Postiz.Plugs.t()],
          post: [Postiz.Post.t()],
          submitted_post: [Postiz.Post.t()],
          sets: [Postiz.Sets.t()],
          signatures: [Postiz.Signatures.t()],
          tags: [Postiz.Tags.t()],
          third_party: [Postiz.ThirdParty.t()],
          used_codes: [Postiz.UsedCodes.t()],
          users: [Postiz.UserOrganization.t()],
          webhooks: [Postiz.Webhooks.t()],
          oauth_app: [Postiz.OAuthApp.t()],
          oauth_authorizations: [Postiz.OAuthAuthorization.t()]
        }

  schema "Organization" do
    field(:name, :string)
    field(:description, :string)
    field(:api_key, :string, source: :apiKey)
    field(:payment_id, :string, source: :paymentId)
    field(:streak_since, Postiz.PrismaDateTime, source: :streakSince)
    field(:deleted_at, Postiz.PrismaDateTime, source: :deletedAt)
    field(:allow_trial, :boolean, default: false, source: :allowTrial)
    field(:is_trailing, :boolean, default: false, source: :isTrailing)
    field(:shortlink, Ecto.Enum, values: [:ASK, :YES, :NO], default: :ASK)
    has_one(:subscription, Postiz.Subscription, foreign_key: :organization_id)
    has_many(:auto_post, Postiz.AutoPost, foreign_key: :organization_id)
    has_many(:comments, Postiz.Comments, foreign_key: :organization_id)
    has_many(:credits, Postiz.Credits, foreign_key: :organization_id)
    has_many(:clippings, Postiz.Clipping, foreign_key: :organization_id)
    has_many(:customers, Postiz.Customer, foreign_key: :org_id)
    has_many(:errors, Postiz.Errors, foreign_key: :organization_id)
    has_many(:github, Postiz.GitHub, foreign_key: :organization_id)
    has_many(:integration, Postiz.Integration, foreign_key: :organization_id)
    has_many(:media, Postiz.Media, foreign_key: :organization_id)
    has_many(:buyer_organization, Postiz.MessagesGroup, foreign_key: :buyer_organization_id)
    has_many(:notifications, Postiz.Notifications, foreign_key: :organization_id)
    has_many(:plugs, Postiz.Plugs, foreign_key: :organization_id)
    has_many(:post, Postiz.Post, foreign_key: :organization_id)
    has_many(:submitted_post, Postiz.Post, foreign_key: :submitted_for_organization_id)
    has_many(:sets, Postiz.Sets, foreign_key: :organization_id)
    has_many(:signatures, Postiz.Signatures, foreign_key: :organization_id)
    has_many(:tags, Postiz.Tags, foreign_key: :org_id)
    has_many(:third_party, Postiz.ThirdParty, foreign_key: :organization_id)
    has_many(:used_codes, Postiz.UsedCodes, foreign_key: :org_id)
    has_many(:users, Postiz.UserOrganization, foreign_key: :organization_id)
    has_many(:webhooks, Postiz.Webhooks, foreign_key: :organization_id)
    has_many(:oauth_app, Postiz.OAuthApp, foreign_key: :organization_id)
    has_many(:oauth_authorizations, Postiz.OAuthAuthorization, foreign_key: :organization_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end