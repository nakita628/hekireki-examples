defmodule LiteLLM.LiteLLMInvitationLink do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          is_accepted: boolean(),
          accepted_at: DateTime.t() | nil,
          expires_at: DateTime.t(),
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          lite_llm_user_table_user: LiteLLM.LiteLLMUserTable.t() | nil,
          lite_llm_user_table_created: LiteLLM.LiteLLMUserTable.t() | nil,
          lite_llm_user_table_updated: LiteLLM.LiteLLMUserTable.t() | nil
        }

  schema "LiteLLM_InvitationLink" do
    field(:is_accepted, :boolean, default: false)
    field(:accepted_at, LiteLLM.PrismaDateTime)
    field(:expires_at, LiteLLM.PrismaDateTime)
    field(:created_at, LiteLLM.PrismaDateTime)
    field(:updated_at, LiteLLM.PrismaDateTime)
    belongs_to(:lite_llm_user_table_user, LiteLLM.LiteLLMUserTable, foreign_key: :user_id, type: :string, references: :user_id)
    belongs_to(:lite_llm_user_table_created, LiteLLM.LiteLLMUserTable, foreign_key: :created_by, type: :string, references: :user_id)
    belongs_to(:lite_llm_user_table_updated, LiteLLM.LiteLLMUserTable, foreign_key: :updated_by, type: :string, references: :user_id)
  end
end