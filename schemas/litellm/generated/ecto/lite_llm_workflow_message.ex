defmodule LiteLLM.LiteLLMWorkflowMessage do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :message_id}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          role: String.t(),
          content: String.t(),
          sequence_number: integer(),
          session_id: String.t() | nil,
          inserted_at: DateTime.t(),
          run: LiteLLM.LiteLLMWorkflowRun.t() | nil
        }

  schema "LiteLLM_WorkflowMessage" do
    field(:role, :string)
    field(:content, :string)
    field(:sequence_number, :integer)
    field(:session_id, :string)
    belongs_to(:run, LiteLLM.LiteLLMWorkflowRun, foreign_key: :run_id)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end