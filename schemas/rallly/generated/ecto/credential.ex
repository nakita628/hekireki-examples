defmodule Rallly.Credential do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          provider: String.t(),
          provider_account_id: String.t(),
          type: atom(),
          secret: String.t(),
          scopes: [String.t()],
          expires_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Rallly.User.t() | nil,
          calendar_connections: [Rallly.CalendarConnection.t()],
          conferencing_connections: [Rallly.ConferencingConnection.t()]
        }

  schema "credentials" do
    field(:id, :string, primary_key: true)
    field(:provider, :string)
    field(:provider_account_id, :string)
    field(:type, Ecto.Enum, values: [:OAUTH])
    field(:secret, :string)
    field(:scopes, {:array, :string})
    field(:expires_at, Rallly.PrismaDateTime)
    belongs_to(:user, Rallly.User, foreign_key: :user_id, type: :string)
    has_many(:calendar_connections, Rallly.CalendarConnection, foreign_key: :credential_id, references: :id)
    has_many(:conferencing_connections, Rallly.ConferencingConnection, foreign_key: :credential_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end