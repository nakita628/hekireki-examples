defmodule Teable.OAuthAppSecret do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          secret: String.t(),
          masked_secret: String.t(),
          created_time: DateTime.t(),
          created_by: String.t(),
          last_used_time: DateTime.t() | nil,
          o_auth_app: Teable.OAuthApp.t() | nil,
          tokens: [Teable.OAuthAppToken.t()]
        }

  schema "oauth_app_secret" do
    field(:id, :string, primary_key: true)
    field(:secret, :string)
    field(:masked_secret, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
    field(:last_used_time, Teable.PrismaDateTime)
    belongs_to(:o_auth_app, Teable.OAuthApp, foreign_key: :client_id, type: :string, references: :client_id)
    has_many(:tokens, Teable.OAuthAppToken, foreign_key: :app_secret_id)
  end
end