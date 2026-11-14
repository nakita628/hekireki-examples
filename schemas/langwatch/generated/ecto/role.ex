defmodule LangWatch.Role do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          permissions: map(),
          kind: String.t(),
          occurred_at: DateTime.t(),
          deleted_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "Role" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:name, :string)
    field(:description, :string)
    field(:permissions, :map)
    field(:kind, :string, default: "custom")
    field(:occurred_at, LangWatch.PrismaDateTime, source: :occurredAt)
    field(:deleted_at, LangWatch.PrismaDateTime, source: :deletedAt)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end