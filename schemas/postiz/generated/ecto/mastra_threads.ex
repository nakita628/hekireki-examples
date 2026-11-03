defmodule Postiz.MastraThreads do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          resource_id: String.t(),
          title: String.t(),
          metadata: String.t() | nil,
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          created_at_z: DateTime.t() | nil,
          updated_at_z: DateTime.t() | nil
        }

  schema "mastra_threads" do
    field(:id, :string, primary_key: true)
    field(:resource_id, :string, source: :resourceId)
    field(:title, :string)
    field(:metadata, :string)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:updated_at, Postiz.PrismaDateTime, source: :updatedAt)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
    field(:updated_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :updatedAtZ)
  end
end