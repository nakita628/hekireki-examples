defmodule Chibisafe.Statistics do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Chibisafe.PrismaDateTime, autogenerate: {Chibisafe.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          batch_id: integer() | nil,
          type: String.t() | nil,
          data: String.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "statistics" do
    field(:batch_id, :integer, source: :batchId)
    field(:type, :string)
    field(:data, :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end