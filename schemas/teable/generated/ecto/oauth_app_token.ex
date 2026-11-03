defmodule Teable.OAuthAppToken do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          refresh_token_sign: String.t(),
          expired_time: DateTime.t(),
          created_time: DateTime.t(),
          created_by: String.t(),
          o_auth_app_secret: Teable.OAuthAppSecret.t() | nil,
          o_auth_app: Teable.OAuthApp.t() | nil
        }

  schema "oauth_app_token" do
    field(:id, :string, primary_key: true)
    field(:refresh_token_sign, :string)
    field(:expired_time, Teable.PrismaDateTime)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
    belongs_to(:o_auth_app_secret, Teable.OAuthAppSecret, foreign_key: :app_secret_id, type: :string)
    belongs_to(:o_auth_app, Teable.OAuthApp, foreign_key: :client_id, type: :string, references: :client_id)
  end
end