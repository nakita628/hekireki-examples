defmodule Rallly.Account do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          provider: String.t(),
          provider_account_id: String.t(),
          refresh_token: String.t() | nil,
          access_token: String.t() | nil,
          expires_at: integer() | nil,
          scope: String.t() | nil,
          id_token: String.t() | nil,
          access_token_expires_at: DateTime.t() | nil,
          refresh_token_expires_at: DateTime.t() | nil,
          password: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Rallly.User.t() | nil
        }

  schema "accounts" do
    field(:id, :string, primary_key: true)
    field(:provider, :string)
    field(:provider_account_id, :string)
    field(:refresh_token, :string)
    field(:access_token, :string)
    field(:expires_at, :integer)
    field(:scope, :string)
    field(:id_token, :string)
    field(:access_token_expires_at, Rallly.PrismaDateTime)
    field(:refresh_token_expires_at, Rallly.PrismaDateTime)
    field(:password, :string)
    belongs_to(:user, Rallly.User, foreign_key: :user_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end