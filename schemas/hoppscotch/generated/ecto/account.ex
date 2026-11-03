defmodule Hoppscotch.Account do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          provider: String.t(),
          provider_account_id: String.t(),
          provider_refresh_token: String.t() | nil,
          provider_access_token: String.t() | nil,
          provider_scope: String.t() | nil,
          logged_in: DateTime.t(),
          user: Hoppscotch.User.t() | nil
        }

  schema "Account" do
    field(:id, :string, primary_key: true)
    field(:provider, :string)
    field(:provider_account_id, :string, source: :providerAccountId)
    field(:provider_refresh_token, :string, source: :providerRefreshToken)
    field(:provider_access_token, :string, source: :providerAccessToken)
    field(:provider_scope, :string, source: :providerScope)
    field(:logged_in, Hoppscotch.PrismaDateTime, autogenerate: true, source: :loggedIn)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, Hoppscotch.User, foreign_key: :user_id, define_field: false, type: :string, references: :uid)
  end
end