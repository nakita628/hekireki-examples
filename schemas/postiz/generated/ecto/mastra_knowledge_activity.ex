defmodule Postiz.MastraKnowledgeActivity do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          action: String.t(),
          record_type: String.t(),
          record_id: String.t(),
          scope: map(),
          scope_key: String.t(),
          source_thread_id: String.t() | nil,
          created_at: DateTime.t(),
          created_at_z: DateTime.t() | nil
        }

  schema "mastra_knowledge_activity" do
    field(:id, :string, primary_key: true)
    field(:action, :string)
    field(:record_type, :string, source: :recordType)
    field(:record_id, :string, source: :recordId)
    field(:scope, :map)
    field(:scope_key, :string, source: :scopeKey)
    field(:source_thread_id, :string, source: :sourceThreadId)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
  end
end