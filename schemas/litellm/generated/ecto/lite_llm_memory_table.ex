defmodule LiteLLM.LiteLLMMemoryTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :memory_id}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          key: String.t(),
          value: String.t(),
          metadata: map() | nil,
          user_id: String.t() | nil,
          team_id: String.t() | nil,
          created_by: String.t() | nil,
          updated_by: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_MemoryTable" do
    field(:key, :string)
    field(:value, :string)
    field(:metadata, :map)
    field(:user_id, :string)
    field(:team_id, :string)
    field(:created_by, :string)
    field(:updated_by, :string)
    timestamps(inserted_at_source: :created_at)
  end
end