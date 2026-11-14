defmodule LangWatch.PlatformToolPolicy do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          tool_slug: String.t(),
          allow_vk: boolean(),
          allow_otel_direct: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "PlatformToolPolicy" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:tool_slug, :string, source: :toolSlug)
    field(:allow_vk, :boolean, default: true, source: :allowVk)
    field(:allow_otel_direct, :boolean, default: true, source: :allowOtelDirect)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end