defmodule LangWatch.OrganizationSeatLease do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          organization_id: String.t(),
          holder: String.t(),
          expires_at: DateTime.t(),
          inserted_at: DateTime.t()
        }

  schema "OrganizationSeatLease" do
    field(:organization_id, :string, primary_key: true, source: :organizationId)
    field(:holder, :string)
    field(:expires_at, LangWatch.PrismaDateTime, source: :expiresAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end