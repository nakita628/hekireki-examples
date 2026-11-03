defmodule Postiz.MastraThreadState do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          thread_id: String.t(),
          type: String.t(),
          value: map(),
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          created_at_z: DateTime.t() | nil,
          updated_at_z: DateTime.t() | nil
        }

  schema "mastra_thread_state" do
    field(:thread_id, :string, primary_key: true, source: :threadId)
    field(:type, :string, primary_key: true)
    field(:value, :map)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:updated_at, Postiz.PrismaDateTime, source: :updatedAt)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
    field(:updated_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :updatedAtZ)
  end
end