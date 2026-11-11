defmodule LiteLLM.LiteLLMWorkflowRun do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :run_id}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          session_id: String.t(),
          workflow_type: String.t(),
          status: String.t(),
          created_by: String.t() | nil,
          input: map() | nil,
          output: map() | nil,
          metadata: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          events: [LiteLLM.LiteLLMWorkflowEvent.t()],
          messages: [LiteLLM.LiteLLMWorkflowMessage.t()]
        }

  schema "LiteLLM_WorkflowRun" do
    field(:session_id, :string, autogenerate: {Ecto.UUID, :generate, []})
    field(:workflow_type, :string)
    field(:status, :string, default: "pending")
    field(:created_by, :string)
    field(:input, :map)
    field(:output, :map)
    field(:metadata, :map)
    has_many(:events, LiteLLM.LiteLLMWorkflowEvent, foreign_key: :run_id)
    has_many(:messages, LiteLLM.LiteLLMWorkflowMessage, foreign_key: :run_id)
    timestamps(inserted_at_source: :created_at)
  end
end