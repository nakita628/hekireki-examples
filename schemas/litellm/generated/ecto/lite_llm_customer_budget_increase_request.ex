defmodule LiteLLM.LiteLLMCustomerBudgetIncreaseRequest do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :request_id}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          requested_max_budget: float(),
          expires_at: DateTime.t() | nil,
          reason: String.t() | nil,
          status: String.t(),
          submitted_by: String.t(),
          submitted_at: DateTime.t(),
          reviewed_by: String.t() | nil,
          reviewed_at: DateTime.t() | nil,
          review_notes: String.t() | nil,
          customer_member: LiteLLM.LiteLLMCustomerMemberTable.t() | nil
        }

  schema "LiteLLM_CustomerBudgetIncreaseRequest" do
    field(:requested_max_budget, :float)
    field(:expires_at, LiteLLM.PrismaDateTime)
    field(:reason, :string)
    field(:status, :string, default: "pending_review")
    field(:submitted_by, :string)
    field(:submitted_at, LiteLLM.PrismaDateTime, autogenerate: true)
    field(:reviewed_by, :string)
    field(:reviewed_at, LiteLLM.PrismaDateTime)
    field(:review_notes, :string)
    belongs_to(:customer_member, LiteLLM.LiteLLMCustomerMemberTable, foreign_key: :customer_member_id)
  end
end