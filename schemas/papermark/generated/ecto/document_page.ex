defmodule Papermark.DocumentPage do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          page_number: integer(),
          embedded_links: [String.t()],
          page_links: map() | nil,
          metadata: map() | nil,
          file: String.t(),
          storage_type: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          version: Papermark.DocumentVersion.t() | nil
        }

  schema "DocumentPage" do
    field(:id, :string, primary_key: true)
    field(:page_number, :integer, source: :pageNumber)
    field(:embedded_links, {:array, :string}, source: :embeddedLinks)
    field(:page_links, :map, source: :pageLinks)
    field(:metadata, :map)
    field(:file, :string)
    field(:storage_type, Ecto.Enum, values: [:S3_PATH, :VERCEL_BLOB], default: :VERCEL_BLOB, source: :storageType)
    field(:version_id, :string, source: :versionId)
    belongs_to(:version, Papermark.DocumentVersion, foreign_key: :version_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end