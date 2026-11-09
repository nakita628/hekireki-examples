defmodule Jitsu.ConfigurationObject do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Jitsu.PrismaDateTime, autogenerate: {Jitsu.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          deleted: boolean() | nil,
          type: String.t(),
          config: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Jitsu.Workspace.t() | nil,
          from_links: [Jitsu.ConfigurationObjectLink.t()],
          to_links: [Jitsu.ConfigurationObjectLink.t()],
          profile_builder: [Jitsu.ProfileBuilder.t()],
          profile_builder_function: [Jitsu.ProfileBuilderFunction.t()]
        }

  schema "ConfigurationObject" do
    field(:id, :string, primary_key: true)
    field(:deleted, :boolean, default: false)
    field(:type, :string)
    field(:config, :map)
    field(:workspace_id, :string, source: :workspaceId)
    belongs_to(:workspace, Jitsu.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    has_many(:from_links, Jitsu.ConfigurationObjectLink, foreign_key: :to_id, references: :id)
    has_many(:to_links, Jitsu.ConfigurationObjectLink, foreign_key: :from_id, references: :id)
    has_many(:profile_builder, Jitsu.ProfileBuilder, foreign_key: :destination_id, references: :id)
    has_many(:profile_builder_function, Jitsu.ProfileBuilderFunction, foreign_key: :function_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end