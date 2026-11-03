defmodule Hoppscotch.PublishedDocs do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Hoppscotch.PrismaDateTime, autogenerate: {Hoppscotch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          slug: String.t(),
          title: String.t(),
          collection_id: String.t(),
          creator_uid: String.t(),
          version: String.t(),
          auto_sync: boolean(),
          document_tree: map() | nil,
          workspace_type: atom(),
          workspace_id: String.t(),
          environment_id: String.t() | nil,
          environment_name: String.t() | nil,
          environment_variables: map() | nil,
          metadata: map() | nil,
          created_on: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "PublishedDocs" do
    field(:id, :string, primary_key: true)
    field(:slug, :string)
    field(:title, :string)
    field(:collection_id, :string, source: :collectionID)
    field(:creator_uid, :string, source: :creatorUid)
    field(:version, :string)
    field(:auto_sync, :boolean, source: :autoSync)
    field(:document_tree, :map, source: :documentTree)
    field(:workspace_type, Ecto.Enum, values: [:USER, :TEAM], source: :workspaceType)
    field(:workspace_id, :string, source: :workspaceID)
    field(:environment_id, :string, source: :environmentID)
    field(:environment_name, :string, source: :environmentName)
    field(:environment_variables, :map, source: :environmentVariables)
    field(:metadata, :map)
    field(:created_on, Hoppscotch.PrismaDateTime, autogenerate: true, source: :createdOn)
    timestamps(inserted_at: false, updated_at_source: :updatedOn)
  end
end