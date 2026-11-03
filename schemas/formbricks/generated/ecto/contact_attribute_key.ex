defmodule Formbricks.ContactAttributeKey do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          is_unique: boolean(),
          key: String.t(),
          name: String.t() | nil,
          description: String.t() | nil,
          type: atom(),
          data_type: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Formbricks.Workspace.t() | nil,
          attributes: [Formbricks.ContactAttribute.t()],
          attribute_filters: [Formbricks.SurveyAttributeFilter.t()]
        }

  schema "ContactAttributeKey" do
    field(:id, :string, primary_key: true)
    field(:is_unique, :boolean, default: false, source: :isUnique)
    field(:key, :string)
    field(:name, :string)
    field(:description, :string)
    field(:type, Ecto.Enum, values: [:default, :custom], default: :custom)
    field(:data_type, Ecto.Enum, values: [:string, :number, :date], default: :string, source: :dataType)
    field(:workspace_id, :string, source: :workspaceId)
    belongs_to(:workspace, Formbricks.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    has_many(:attributes, Formbricks.ContactAttribute, foreign_key: :attribute_key_id)
    has_many(:attribute_filters, Formbricks.SurveyAttributeFilter, foreign_key: :attribute_key_id)
    timestamps(inserted_at_source: :created_at)
  end
end