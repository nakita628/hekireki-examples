defmodule LangWatch.SsoCredential do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          connection_id: String.t(),
          kind: String.t(),
          ciphertext: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "SsoCredential" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:connection_id, :string, source: :connectionId)
    field(:kind, :string)
    field(:ciphertext, :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end