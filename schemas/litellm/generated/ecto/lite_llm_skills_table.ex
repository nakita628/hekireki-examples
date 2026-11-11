defmodule LiteLLM.LiteLLMSkillsTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :skill_id}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          display_title: String.t() | nil,
          description: String.t() | nil,
          instructions: String.t() | nil,
          source: String.t(),
          latest_version: String.t() | nil,
          file_content: binary() | nil,
          file_name: String.t() | nil,
          file_type: String.t() | nil,
          metadata: map() | nil,
          created_by: String.t() | nil,
          updated_by: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_SkillsTable" do
    field(:display_title, :string)
    field(:description, :string)
    field(:instructions, :string)
    field(:source, :string, default: "custom")
    field(:latest_version, :string)
    field(:file_content, :binary)
    field(:file_name, :string)
    field(:file_type, :string)
    field(:metadata, :map, default: %{})
    field(:created_by, :string)
    field(:updated_by, :string)
    timestamps(inserted_at_source: :created_at)
  end
end