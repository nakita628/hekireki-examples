defmodule Postiz.OAuthApp do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Postiz.PrismaDateTime, autogenerate: {Postiz.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          name: String.t(),
          description: String.t() | nil,
          redirect_url: String.t(),
          redirect_uris: String.t() | nil,
          client_id: String.t(),
          client_secret: String.t() | nil,
          dynamic: boolean(),
          token_endpoint_auth_method: String.t() | nil,
          deleted_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Postiz.Organization.t() | nil,
          picture: Postiz.Media.t() | nil,
          authorizations: [Postiz.OAuthAuthorization.t()],
          self_hosted_authorizations: [Postiz.OAuthSelfHostedAuthorization.t()]
        }

  schema "OAuthApp" do
    field(:name, :string)
    field(:description, :string)
    field(:redirect_url, :string, source: :redirectUrl)
    field(:redirect_uris, :string, source: :redirectUris)
    field(:client_id, :string, source: :clientId)
    field(:client_secret, :string, source: :clientSecret)
    field(:dynamic, :boolean, default: false)
    field(:token_endpoint_auth_method, :string, source: :tokenEndpointAuthMethod)
    field(:deleted_at, Postiz.PrismaDateTime, source: :deletedAt)
    field(:organization_id, :binary_id, source: :organizationId)
    field(:picture_id, :binary_id, source: :pictureId)
    belongs_to(:organization, Postiz.Organization, foreign_key: :organization_id, define_field: false)
    belongs_to(:picture, Postiz.Media, foreign_key: :picture_id, define_field: false)
    has_many(:authorizations, Postiz.OAuthAuthorization, foreign_key: :oauth_app_id)
    has_many(:self_hosted_authorizations, Postiz.OAuthSelfHostedAuthorization, foreign_key: :oauth_app_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end