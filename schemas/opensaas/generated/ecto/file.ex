defmodule OpenSaas.File do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: OpenSaas.PrismaDateTime, autogenerate: {OpenSaas.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          name: String.t(),
          type: String.t(),
          s3_key: String.t(),
          inserted_at: DateTime.t(),
          user: OpenSaas.User.t() | nil
        }

  schema "File" do
    field(:name, :string)
    field(:type, :string)
    field(:s3_key, :string, source: :s3Key)
    field(:user_id, :binary_id, source: :userId)
    belongs_to(:user, OpenSaas.User, foreign_key: :user_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end