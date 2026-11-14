defmodule LangWatch.Account do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: String.t(),
          provider: String.t(),
          issuer: String.t() | nil,
          provider_account_id: String.t(),
          refresh_token: String.t() | nil,
          access_token: String.t() | nil,
          expires_at: DateTime.t() | nil,
          ext_expires_in: integer() | nil,
          token_type: String.t() | nil,
          scope: String.t() | nil,
          id_token: String.t() | nil,
          session_state: String.t() | nil,
          password: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: LangWatch.User.t() | nil
        }

  schema "Account" do
    field(:id, :string, primary_key: true)
    field(:type, :string, default: "oauth")
    field(:provider, :string)
    field(:issuer, :string)
    field(:provider_account_id, :string, source: :providerAccountId)
    field(:refresh_token, :string)
    field(:access_token, :string)
    field(:expires_at, LangWatch.PrismaDateTime)
    field(:ext_expires_in, :integer)
    field(:token_type, :string)
    field(:scope, :string)
    field(:id_token, :string)
    field(:session_state, :string)
    field(:password, :string)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, LangWatch.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end