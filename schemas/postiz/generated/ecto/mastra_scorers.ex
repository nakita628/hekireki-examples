defmodule Postiz.MastraScorers do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          scorer_id: String.t(),
          trace_id: String.t() | nil,
          run_id: String.t(),
          scorer: map(),
          preprocess_step_result: map() | nil,
          extract_step_result: map() | nil,
          analyze_step_result: map() | nil,
          score: float(),
          reason: String.t() | nil,
          metadata: map() | nil,
          preprocess_prompt: String.t() | nil,
          extract_prompt: String.t() | nil,
          generate_score_prompt: String.t() | nil,
          generate_reason_prompt: String.t() | nil,
          analyze_prompt: String.t() | nil,
          reason_prompt: String.t() | nil,
          input: map(),
          output: map(),
          additional_context: map() | nil,
          runtime_context: map() | nil,
          entity_type: String.t() | nil,
          entity: map() | nil,
          entity_id: String.t() | nil,
          source: String.t(),
          resource_id: String.t() | nil,
          thread_id: String.t() | nil,
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          created_at_z: DateTime.t() | nil,
          updated_at_z: DateTime.t() | nil,
          span_id: String.t() | nil,
          request_context: map() | nil,
          organization_id: String.t() | nil,
          project_id: String.t() | nil,
          batch_id: String.t() | nil,
          dataset_id: String.t() | nil,
          dataset_item_id: String.t() | nil
        }

  schema "mastra_scorers" do
    field(:id, :string, primary_key: true)
    field(:scorer_id, :string, source: :scorerId)
    field(:trace_id, :string, source: :traceId)
    field(:run_id, :string, source: :runId)
    field(:scorer, :map)
    field(:preprocess_step_result, :map, source: :preprocessStepResult)
    field(:extract_step_result, :map, source: :extractStepResult)
    field(:analyze_step_result, :map, source: :analyzeStepResult)
    field(:score, :float)
    field(:reason, :string)
    field(:metadata, :map)
    field(:preprocess_prompt, :string, source: :preprocessPrompt)
    field(:extract_prompt, :string, source: :extractPrompt)
    field(:generate_score_prompt, :string, source: :generateScorePrompt)
    field(:generate_reason_prompt, :string, source: :generateReasonPrompt)
    field(:analyze_prompt, :string, source: :analyzePrompt)
    field(:reason_prompt, :string, source: :reasonPrompt)
    field(:input, :map)
    field(:output, :map)
    field(:additional_context, :map, source: :additionalContext)
    field(:runtime_context, :map, source: :runtimeContext)
    field(:entity_type, :string, source: :entityType)
    field(:entity, :map)
    field(:entity_id, :string, source: :entityId)
    field(:source, :string)
    field(:resource_id, :string, source: :resourceId)
    field(:thread_id, :string, source: :threadId)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:updated_at, Postiz.PrismaDateTime, source: :updatedAt)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
    field(:updated_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :updatedAtZ)
    field(:span_id, :string, source: :spanId)
    field(:request_context, :map, source: :requestContext)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    field(:batch_id, :string, source: :batchId)
    field(:dataset_id, :string, source: :datasetId)
    field(:dataset_item_id, :string, source: :datasetItemId)
  end
end