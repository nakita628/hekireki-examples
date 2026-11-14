defmodule LangWatch.DepartmentMembershipHistory do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          user_id: String.t(),
          department_id: String.t(),
          valid_from: DateTime.t(),
          valid_to: DateTime.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "DepartmentMembershipHistory" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:user_id, :string, source: :userId)
    field(:department_id, :string, source: :departmentId)
    field(:valid_from, LangWatch.PrismaDateTime, source: :validFrom)
    field(:valid_to, LangWatch.PrismaDateTime, source: :validTo)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end