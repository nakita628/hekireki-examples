defmodule Linkwarden.Tag do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Linkwarden.PrismaDateTime, autogenerate: {Linkwarden.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          archive_as_screenshot: boolean() | nil,
          archive_as_monolith: boolean() | nil,
          archive_as_pdf: boolean() | nil,
          archive_as_readable: boolean() | nil,
          archive_as_wayback_machine: boolean() | nil,
          ai_tag: boolean() | nil,
          ai_generated: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          owner: Linkwarden.User.t() | nil,
          links: [Linkwarden.Link.t()]
        }

  schema "Tag" do
    field(:name, :string)
    field(:archive_as_screenshot, :boolean, source: :archiveAsScreenshot)
    field(:archive_as_monolith, :boolean, source: :archiveAsMonolith)
    field(:archive_as_pdf, :boolean, source: :archiveAsPDF)
    field(:archive_as_readable, :boolean, source: :archiveAsReadable)
    field(:archive_as_wayback_machine, :boolean, source: :archiveAsWaybackMachine)
    field(:ai_tag, :boolean, source: :aiTag)
    field(:ai_generated, :boolean, default: false, source: :aiGenerated)
    field(:owner_id, :id, source: :ownerId)
    belongs_to(:owner, Linkwarden.User, foreign_key: :owner_id, define_field: false)
    many_to_many(:links, Linkwarden.Link, join_through: "_LinkToTag", join_keys: [B: :id, A: :id])
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end