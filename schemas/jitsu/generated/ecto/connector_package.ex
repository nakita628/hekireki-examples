defmodule Jitsu.ConnectorPackage do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Jitsu.PrismaDateTime, autogenerate: {Jitsu.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          package_id: String.t(),
          package_type: String.t(),
          meta: map() | nil,
          logo_svg: binary() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "ConnectorPackage" do
    field(:id, :string, primary_key: true)
    field(:package_id, :string, source: :packageId)
    field(:package_type, :string, default: "airbyte", source: :packageType)
    field(:meta, :map)
    field(:logo_svg, :binary, source: :logoSvg)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end