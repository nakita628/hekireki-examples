defmodule Postiz.MastraWorkflowDefinitions do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          description: String.t() | nil,
          metadata: map() | nil,
          input_schema: map(),
          output_schema: map(),
          state_schema: map() | nil,
          request_context_schema: map() | nil,
          graph: map(),
          schedule: map() | nil,
          status: String.t(),
          source: String.t(),
          author_id: String.t() | nil,
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          created_at_z: DateTime.t() | nil,
          updated_at_z: DateTime.t() | nil
        }

  schema "mastra_workflow_definitions" do
    field(:id, :string, primary_key: true)
    field(:description, :string)
    field(:metadata, :map)
    field(:input_schema, :map, source: :inputSchema)
    field(:output_schema, :map, source: :outputSchema)
    field(:state_schema, :map, source: :stateSchema)
    field(:request_context_schema, :map, source: :requestContextSchema)
    field(:graph, :map)
    field(:schedule, :map)
    field(:status, :string)
    field(:source, :string)
    field(:author_id, :string, source: :authorId)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:updated_at, Postiz.PrismaDateTime, source: :updatedAt)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
    field(:updated_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :updatedAtZ)
  end
end