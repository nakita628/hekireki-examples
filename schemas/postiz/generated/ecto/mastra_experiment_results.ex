defmodule Postiz.MastraExperimentResults do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          experiment_id: String.t(),
          item_id: String.t(),
          item_dataset_version: integer() | nil,
          input: map(),
          output: map() | nil,
          ground_truth: map() | nil,
          error: map() | nil,
          started_at: DateTime.t(),
          completed_at: DateTime.t(),
          retry_count: integer(),
          trace_id: String.t() | nil,
          status: String.t() | nil,
          tags: map() | nil,
          created_at: DateTime.t(),
          started_at_z: DateTime.t() | nil,
          completed_at_z: DateTime.t() | nil,
          created_at_z: DateTime.t() | nil,
          comment: String.t() | nil,
          tool_mock_report: map() | nil,
          metadata: map() | nil,
          organization_id: String.t() | nil,
          project_id: String.t() | nil,
          attempt: integer() | nil
        }

  schema "mastra_experiment_results" do
    field(:id, :string, primary_key: true)
    field(:experiment_id, :string, source: :experimentId)
    field(:item_id, :string, source: :itemId)
    field(:item_dataset_version, :integer, source: :itemDatasetVersion)
    field(:input, :map)
    field(:output, :map)
    field(:ground_truth, :map, source: :groundTruth)
    field(:error, :map)
    field(:started_at, Postiz.PrismaDateTime, source: :startedAt)
    field(:completed_at, Postiz.PrismaDateTime, source: :completedAt)
    field(:retry_count, :integer, source: :retryCount)
    field(:trace_id, :string, source: :traceId)
    field(:status, :string)
    field(:tags, :map)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:started_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :startedAtZ)
    field(:completed_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :completedAtZ)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
    field(:comment, :string)
    field(:tool_mock_report, :map, source: :toolMockReport)
    field(:metadata, :map)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    field(:attempt, :integer)
  end
end