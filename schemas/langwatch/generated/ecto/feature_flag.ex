defmodule LangWatch.FeatureFlag do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          key: String.t(),
          enabled: boolean(),
          rules: map() | nil,
          last_edited_by: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "FeatureFlag" do
    field(:key, :string, primary_key: true)
    field(:enabled, :boolean)
    field(:rules, :map)
    field(:last_edited_by, :string, source: :lastEditedBy)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end