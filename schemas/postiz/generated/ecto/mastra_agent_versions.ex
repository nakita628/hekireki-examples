defmodule Postiz.MastraAgentVersions do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          agent_id: String.t(),
          version_number: integer(),
          name: String.t(),
          description: String.t() | nil,
          instructions: String.t(),
          model: map(),
          tools: map() | nil,
          default_options: map() | nil,
          workflows: map() | nil,
          agents: map() | nil,
          integration_tools: map() | nil,
          input_processors: map() | nil,
          output_processors: map() | nil,
          memory: map() | nil,
          scorers: map() | nil,
          mcp_clients: map() | nil,
          request_context_schema: map() | nil,
          workspace: map() | nil,
          skills: map() | nil,
          skills_format: String.t() | nil,
          changed_fields: map() | nil,
          change_message: String.t() | nil,
          created_at: DateTime.t(),
          created_at_z: DateTime.t() | nil,
          durable: map() | nil,
          browser: map() | nil,
          tool_providers: map() | nil
        }

  schema "mastra_agent_versions" do
    field(:id, :string, primary_key: true)
    field(:agent_id, :string, source: :agentId)
    field(:version_number, :integer, source: :versionNumber)
    field(:name, :string)
    field(:description, :string)
    field(:instructions, :string)
    field(:model, :map)
    field(:tools, :map)
    field(:default_options, :map, source: :defaultOptions)
    field(:workflows, :map)
    field(:agents, :map)
    field(:integration_tools, :map, source: :integrationTools)
    field(:input_processors, :map, source: :inputProcessors)
    field(:output_processors, :map, source: :outputProcessors)
    field(:memory, :map)
    field(:scorers, :map)
    field(:mcp_clients, :map, source: :mcpClients)
    field(:request_context_schema, :map, source: :requestContextSchema)
    field(:workspace, :map)
    field(:skills, :map)
    field(:skills_format, :string, source: :skillsFormat)
    field(:changed_fields, :map, source: :changedFields)
    field(:change_message, :string, source: :changeMessage)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
    field(:durable, :map)
    field(:browser, :map)
    field(:tool_providers, :map, source: :toolProviders)
  end
end