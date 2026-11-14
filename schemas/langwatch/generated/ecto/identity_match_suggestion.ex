defmodule LangWatch.IdentityMatchSuggestion do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          discovered_person_id: String.t(),
          user_id: String.t(),
          score: float(),
          computed_at: DateTime.t()
        }

  schema "IdentityMatchSuggestion" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:discovered_person_id, :string, source: :discoveredPersonId)
    field(:user_id, :string, source: :userId)
    field(:score, :float)
    field(:computed_at, LangWatch.PrismaDateTime, source: :computedAt)
  end
end