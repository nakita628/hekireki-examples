defmodule Postiz.MastraKnowledgeRecords do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          node: String.t(),
          text: String.t(),
          scope: map(),
          scope_key: String.t(),
          source_thread_id: String.t(),
          captured_at: DateTime.t(),
          when: DateTime.t() | nil,
          max_scope: String.t() | nil,
          metadata: map() | nil,
          deleted_at: DateTime.t() | nil,
          deleted_by: String.t() | nil,
          captured_at_z: DateTime.t() | nil,
          when_z: DateTime.t() | nil,
          deleted_at_z: DateTime.t() | nil
        }

  schema "mastra_knowledge_records" do
    field(:id, :string, primary_key: true)
    field(:node, :string)
    field(:text, :string)
    field(:scope, :map)
    field(:scope_key, :string, source: :scopeKey)
    field(:source_thread_id, :string, source: :sourceThreadId)
    field(:captured_at, Postiz.PrismaDateTime, source: :capturedAt)
    field(:when, Postiz.PrismaDateTime)
    field(:max_scope, :string, source: :maxScope)
    field(:metadata, :map)
    field(:deleted_at, Postiz.PrismaDateTime, source: :deletedAt)
    field(:deleted_by, :string, source: :deletedBy)
    field(:captured_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :capturedAtZ)
    field(:when_z, Postiz.PrismaDateTime, autogenerate: true, source: :whenZ)
    field(:deleted_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :deletedAtZ)
  end
end