defmodule LiteLLM.LiteLLMAdaptiveRouterSession do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          session_id: String.t(),
          router_name: String.t(),
          model_name: String.t(),
          classified_type: String.t(),
          misalignment_count: integer(),
          stagnation_count: integer(),
          disengagement_count: integer(),
          satisfaction_count: integer(),
          failure_count: integer(),
          loop_count: integer(),
          exhaustion_count: integer(),
          last_user_content: String.t() | nil,
          last_assistant_content: String.t() | nil,
          tool_call_history: map(),
          pending_tool_calls: map(),
          turn_count: integer(),
          last_processed_turn: integer(),
          clean_credit_awarded: boolean(),
          terminal_status: integer() | nil,
          updated_at: DateTime.t()
        }

  schema "LiteLLM_AdaptiveRouterSession" do
    field(:session_id, :string, primary_key: true)
    field(:router_name, :string, primary_key: true)
    field(:model_name, :string, primary_key: true)
    field(:classified_type, :string)
    field(:misalignment_count, :integer, default: 0)
    field(:stagnation_count, :integer, default: 0)
    field(:disengagement_count, :integer, default: 0)
    field(:satisfaction_count, :integer, default: 0)
    field(:failure_count, :integer, default: 0)
    field(:loop_count, :integer, default: 0)
    field(:exhaustion_count, :integer, default: 0)
    field(:last_user_content, :string)
    field(:last_assistant_content, :string)
    field(:tool_call_history, :map)
    field(:pending_tool_calls, :map, default: %{})
    field(:turn_count, :integer, default: 0)
    field(:last_processed_turn, :integer, default: -1)
    field(:clean_credit_awarded, :boolean, default: false)
    field(:terminal_status, :integer)
    timestamps(inserted_at: false, updated_at_source: :last_activity_at)
  end
end