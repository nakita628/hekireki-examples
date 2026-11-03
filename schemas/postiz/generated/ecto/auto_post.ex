defmodule Postiz.AutoPost do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Postiz.PrismaDateTime, autogenerate: {Postiz.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          title: String.t(),
          content: String.t() | nil,
          on_slot: boolean(),
          sync_last: boolean(),
          url: String.t(),
          last_url: String.t(),
          active: boolean(),
          add_picture: boolean(),
          generate_content: boolean(),
          integrations: String.t(),
          deleted_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Postiz.Organization.t() | nil
        }

  schema "AutoPost" do
    field(:title, :string)
    field(:content, :string)
    field(:on_slot, :boolean, source: :onSlot)
    field(:sync_last, :boolean, source: :syncLast)
    field(:url, :string)
    field(:last_url, :string, source: :lastUrl)
    field(:active, :boolean)
    field(:add_picture, :boolean, source: :addPicture)
    field(:generate_content, :boolean, source: :generateContent)
    field(:integrations, :string)
    field(:deleted_at, Postiz.PrismaDateTime, source: :deletedAt)
    field(:organization_id, :binary_id, source: :organizationId)
    belongs_to(:organization, Postiz.Organization, foreign_key: :organization_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end