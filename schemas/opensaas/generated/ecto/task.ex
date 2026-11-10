defmodule OpenSaas.Task do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: OpenSaas.PrismaDateTime, autogenerate: {OpenSaas.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          description: String.t(),
          time: String.t(),
          is_done: boolean(),
          inserted_at: DateTime.t(),
          user: OpenSaas.User.t() | nil
        }

  schema "Task" do
    field(:description, :string)
    field(:time, :string, default: "1")
    field(:is_done, :boolean, default: false, source: :isDone)
    field(:user_id, :binary_id, source: :userId)
    belongs_to(:user, OpenSaas.User, foreign_key: :user_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end