defmodule Typebot.Result do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          variables: map(),
          is_completed: boolean(),
          has_started: boolean() | nil,
          is_archived: boolean() | nil,
          last_chat_session_id: String.t() | nil,
          inserted_at: DateTime.t(),
          typebot: Typebot.Typebot.t() | nil,
          answers: [Typebot.Answer.t()],
          answers_v2: [Typebot.AnswerV2.t()],
          logs: [Typebot.Log.t()],
          edges: [Typebot.VisitedEdge.t()],
          set_variable_history: [Typebot.SetVariableHistoryItem.t()]
        }

  schema "Result" do
    field(:id, :string, primary_key: true)
    field(:variables, :map)
    field(:is_completed, :boolean, source: :isCompleted)
    field(:has_started, :boolean, source: :hasStarted)
    field(:is_archived, :boolean, default: false, source: :isArchived)
    field(:last_chat_session_id, :string, source: :lastChatSessionId)
    field(:typebot_id, :string, source: :typebotId)
    belongs_to(:typebot, Typebot.Typebot, foreign_key: :typebot_id, define_field: false, type: :string)
    has_many(:answers, Typebot.Answer, foreign_key: :result_id)
    has_many(:answers_v2, Typebot.AnswerV2, foreign_key: :result_id)
    has_many(:logs, Typebot.Log, foreign_key: :result_id)
    has_many(:edges, Typebot.VisitedEdge, foreign_key: :result_id)
    has_many(:set_variable_history, Typebot.SetVariableHistoryItem, foreign_key: :result_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end