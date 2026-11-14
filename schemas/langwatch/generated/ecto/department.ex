defmodule LangWatch.Department do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          name: String.t(),
          archived_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "Department" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:name, :string)
    field(:archived_at, LangWatch.PrismaDateTime, source: :archivedAt)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end