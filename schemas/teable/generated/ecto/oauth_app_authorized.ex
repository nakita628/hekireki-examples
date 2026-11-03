defmodule Teable.OAuthAppAuthorized do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          user_id: String.t(),
          authorized_time: DateTime.t(),
          o_auth_app: Teable.OAuthApp.t() | nil
        }

  schema "oauth_app_authorized" do
    field(:id, :string, primary_key: true)
    field(:user_id, :string)
    field(:authorized_time, Teable.PrismaDateTime)
    belongs_to(:o_auth_app, Teable.OAuthApp, foreign_key: :client_id, type: :string, references: :client_id)
  end
end