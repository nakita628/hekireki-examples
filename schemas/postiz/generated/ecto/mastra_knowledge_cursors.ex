defmodule Postiz.MastraKnowledgeCursors do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          source_thread_id: String.t(),
          agent: String.t(),
          last_knowledge_id: String.t(),
          updated_at: DateTime.t(),
          updated_at_z: DateTime.t() | nil
        }

  schema "mastra_knowledge_cursors" do
    field(:source_thread_id, :string, primary_key: true, source: :sourceThreadId)
    field(:agent, :string, primary_key: true)
    field(:last_knowledge_id, :string, source: :lastKnowledgeId)
    field(:updated_at, Postiz.PrismaDateTime, source: :updatedAt)
    field(:updated_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :updatedAtZ)
  end
end