defmodule LangWatch.NurturingOrganizationMember do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          organization_id: String.t(),
          user_id: String.t(),
          joined_at: DateTime.t() | nil,
          left_at: DateTime.t() | nil,
          returned_at: DateTime.t() | nil
        }

  schema "NurturingOrganizationMember" do
    field(:organization_id, :string, primary_key: true, source: :organizationId)
    field(:user_id, :string, primary_key: true, source: :userId)
    field(:joined_at, LangWatch.PrismaDateTime, source: :joinedAt)
    field(:left_at, LangWatch.PrismaDateTime, source: :leftAt)
    field(:returned_at, LangWatch.PrismaDateTime, source: :returnedAt)
  end
end