defmodule Jitsu.ProfileBuilder do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Jitsu.PrismaDateTime, autogenerate: {Jitsu.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          deleted: boolean() | nil,
          intermediate_storage_credentials: map(),
          connection_options: map() | nil,
          version: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Jitsu.Workspace.t() | nil,
          destination: Jitsu.ConfigurationObject.t() | nil,
          functions: [Jitsu.ProfileBuilderFunction.t()]
        }

  schema "ProfileBuilder" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:deleted, :boolean, default: false)
    field(:intermediate_storage_credentials, :map, source: :intermediateStorageCredentials)
    field(:connection_options, :map, source: :connectionOptions)
    field(:version, :integer, default: 0)
    field(:workspace_id, :string, source: :workspaceId)
    field(:destination_id, :string, source: :destinationId)
    belongs_to(:workspace, Jitsu.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    belongs_to(:destination, Jitsu.ConfigurationObject, foreign_key: :destination_id, define_field: false, type: :string)
    has_many(:functions, Jitsu.ProfileBuilderFunction, foreign_key: :profile_builder_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end