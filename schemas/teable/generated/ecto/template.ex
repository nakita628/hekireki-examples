defmodule Teable.Template do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          base_id: String.t() | nil,
          cover: String.t() | nil,
          name: String.t() | nil,
          description: String.t() | nil,
          markdown_description: String.t() | nil,
          category_id: [String.t()],
          created_time: DateTime.t(),
          created_by: String.t(),
          last_modified_by: String.t() | nil,
          is_system: boolean() | nil,
          is_published: boolean() | nil,
          featured: boolean() | nil,
          snapshot: String.t() | nil,
          order: float(),
          usage_count: integer(),
          publish_info: map() | nil,
          visit_count: integer(),
          kind: String.t() | nil,
          version: integer(),
          manifest: map() | nil,
          review: map() | nil,
          history: map() | nil,
          parent_template_id: String.t() | nil,
          slug: String.t() | nil,
          updated_at: DateTime.t() | nil
        }

  schema "template" do
    field(:id, :string, primary_key: true)
    field(:base_id, :string)
    field(:cover, :string)
    field(:name, :string)
    field(:description, :string)
    field(:markdown_description, :string)
    field(:category_id, {:array, :string})
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
    field(:last_modified_by, :string)
    field(:is_system, :boolean)
    field(:is_published, :boolean)
    field(:featured, :boolean)
    field(:snapshot, :string)
    field(:order, :float)
    field(:usage_count, :integer, default: 0)
    field(:publish_info, :map)
    field(:visit_count, :integer, default: 0)
    field(:kind, :string)
    field(:version, :integer, default: 1)
    field(:manifest, :map)
    field(:review, :map)
    field(:history, :map)
    field(:parent_template_id, :string)
    field(:slug, :string)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end