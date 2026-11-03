defmodule Calcom.TempOrgRedirect do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          from: String.t(),
          from_org_id: integer(),
          type: atom(),
          to_url: String.t(),
          enabled: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "TempOrgRedirect" do
    field(:from, :string)
    field(:from_org_id, :integer, source: :fromOrgId)
    field(:type, Ecto.Enum, values: [UserEventType: "user-event-type", TeamEventType: "team-event-type", User: "user", Team: "team"])
    field(:to_url, :string, source: :toUrl)
    field(:enabled, :boolean, default: true)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end