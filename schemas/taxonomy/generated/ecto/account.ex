defmodule Taxonomy.Account do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Taxonomy.PrismaDateTime, autogenerate: {Taxonomy.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: String.t(),
          provider: String.t(),
          provider_account_id: String.t(),
          refresh_token: String.t() | nil,
          access_token: String.t() | nil,
          expires_at: integer() | nil,
          token_type: String.t() | nil,
          scope: String.t() | nil,
          id_token: String.t() | nil,
          session_state: String.t() | nil,
          updated_at: DateTime.t(),
          inserted_at: DateTime.t(),
          user: Taxonomy.User.t() | nil
        }

  schema "accounts" do
    field(:id, :string, primary_key: true)
    field(:type, :string)
    field(:provider, :string)
    field(:provider_account_id, :string, source: :providerAccountId)
    field(:refresh_token, :string)
    field(:access_token, :string)
    field(:expires_at, :integer)
    field(:token_type, :string)
    field(:scope, :string)
    field(:id_token, :string)
    field(:session_state, :string)
    field(:updated_at, Taxonomy.PrismaDateTime, autogenerate: true)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, Taxonomy.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end