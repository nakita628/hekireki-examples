defmodule LangWatch.SsoConnectionRegistrationSlot do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          organization_id: String.t(),
          kind: String.t(),
          connection_id: String.t(),
          replaces_connection_id: String.t() | nil,
          command_id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "SsoConnectionRegistrationSlot" do
    field(:organization_id, :string, primary_key: true, source: :organizationId)
    field(:kind, :string, primary_key: true)
    field(:connection_id, :string, source: :connectionId)
    field(:replaces_connection_id, :string, source: :replacesConnectionId)
    field(:command_id, :string, source: :commandId)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end