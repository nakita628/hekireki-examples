defmodule SuperProductivity.Passkey do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: SuperProductivity.PrismaDateTime, autogenerate: {SuperProductivity.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          credential_id: binary(),
          public_key: binary(),
          counter: integer(),
          transports: String.t() | nil,
          last_used_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          user: SuperProductivity.User.t() | nil
        }

  schema "passkeys" do
    field(:id, :string, primary_key: true)
    field(:credential_id, :binary)
    field(:public_key, :binary)
    field(:counter, :integer, default: 0)
    field(:transports, :string)
    field(:last_used_at, SuperProductivity.PrismaDateTime)
    belongs_to(:user, SuperProductivity.User, foreign_key: :user_id)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end