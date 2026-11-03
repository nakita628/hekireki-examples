defmodule Teable.OAuthApp do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          logo: String.t() | nil,
          homepage: String.t(),
          description: String.t() | nil,
          client_id: String.t(),
          redirect_uris: String.t() | nil,
          scopes: String.t() | nil,
          allow_device_flow: boolean(),
          dynamic_registration: boolean(),
          created_time: DateTime.t(),
          created_by: String.t(),
          updated_at: DateTime.t() | nil,
          secrets: [Teable.OAuthAppSecret.t()],
          authorized: [Teable.OAuthAppAuthorized.t()],
          tokens: [Teable.OAuthAppToken.t()]
        }

  schema "oauth_app" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:logo, :string)
    field(:homepage, :string)
    field(:description, :string)
    field(:client_id, :string)
    field(:redirect_uris, :string)
    field(:scopes, :string)
    field(:allow_device_flow, :boolean, default: false)
    field(:dynamic_registration, :boolean, default: false)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
    has_many(:secrets, Teable.OAuthAppSecret, foreign_key: :client_id, references: :client_id)
    has_many(:authorized, Teable.OAuthAppAuthorized, foreign_key: :client_id, references: :client_id)
    has_many(:tokens, Teable.OAuthAppToken, foreign_key: :client_id, references: :client_id)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end