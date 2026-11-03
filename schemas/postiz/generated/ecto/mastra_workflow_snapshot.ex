defmodule Postiz.MastraWorkflowSnapshot do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          workflow_name: String.t(),
          run_id: String.t(),
          resource_id: String.t() | nil,
          snapshot: String.t(),
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          created_at_z: DateTime.t() | nil,
          updated_at_z: DateTime.t() | nil
        }

  schema "mastra_workflow_snapshot" do
    field(:workflow_name, :string, primary_key: true)
    field(:run_id, :string, primary_key: true)
    field(:resource_id, :string, source: :resourceId)
    field(:snapshot, :string)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:updated_at, Postiz.PrismaDateTime, source: :updatedAt)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
    field(:updated_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :updatedAtZ)
  end
end