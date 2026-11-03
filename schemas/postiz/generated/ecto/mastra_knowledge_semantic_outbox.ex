defmodule Postiz.MastraKnowledgeSemanticOutbox do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          idempotency_key: String.t(),
          document_id: String.t(),
          document_type: String.t(),
          operation: String.t(),
          scope: map(),
          scope_key: String.t(),
          status: String.t(),
          attempts: integer(),
          available_at: DateTime.t(),
          claimed_at: DateTime.t() | nil,
          claimed_by: String.t() | nil,
          created_at: DateTime.t(),
          completed_at: DateTime.t() | nil,
          available_at_z: DateTime.t() | nil,
          claimed_at_z: DateTime.t() | nil,
          created_at_z: DateTime.t() | nil,
          completed_at_z: DateTime.t() | nil
        }

  schema "mastra_knowledge_semantic_outbox" do
    field(:id, :string, primary_key: true)
    field(:idempotency_key, :string, source: :idempotencyKey)
    field(:document_id, :string, source: :documentId)
    field(:document_type, :string, source: :documentType)
    field(:operation, :string)
    field(:scope, :map)
    field(:scope_key, :string, source: :scopeKey)
    field(:status, :string)
    field(:attempts, :integer)
    field(:available_at, Postiz.PrismaDateTime, source: :availableAt)
    field(:claimed_at, Postiz.PrismaDateTime, source: :claimedAt)
    field(:claimed_by, :string, source: :claimedBy)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:completed_at, Postiz.PrismaDateTime, source: :completedAt)
    field(:available_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :availableAtZ)
    field(:claimed_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :claimedAtZ)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
    field(:completed_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :completedAtZ)
  end
end